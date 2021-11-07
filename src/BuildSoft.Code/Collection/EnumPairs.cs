using System.Collections.Immutable;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.CompilerServices;
using System.Runtime.Serialization;

namespace BuildSoft.Code.Collection
{
    public class EnumPairs<TKey> : SortedList<TKey, string>  where TKey : struct, Enum
    {
        private static readonly EnumComparer _comparer = new();

        #region Constractors
        public EnumPairs() : base(_comparer)
        {
        }

        public EnumPairs(IDictionary<TKey, string> dictionary) : base(dictionary, _comparer)
        {
        }

        public EnumPairs(int capacity) : base(capacity, _comparer)
        {
        }
        #endregion

        public IEnumerable<string> GetStrings(TKey value)
        {
            var names = Values.ToArray();
            var keys = Keys.ToArray();
            ulong resultValue = EnumHelper.ToUInt64(TypeCode, value);

            if (resultValue == 0)
            {
                if (keys.Length > 0)
                {
                    ulong v = EnumHelper.ToUInt64(TypeCode, keys[0]);
                    if (v == 0)
                    {
                        yield return names[0];
                    }
                }
                yield break;
            }

            // Walk from largest to smallest. It's common to have a flags enum with a single
            // value that matches a single entry, in which case we can just return the existing
            // name string.
            int index = keys.Length - 1;
            while (index >= 0)
            {
                ulong cmpValue = EnumHelper.ToUInt64(TypeCode, keys[index]);
                if (cmpValue == resultValue)
                {
                    yield return names[index];
                    yield break;
                }

                if (cmpValue < resultValue)
                {
                    break;
                }

                index--;
            }

            int[] foundItems = new int[64];

            int resultLength = 0, foundItemsCount = 0;
            while (index >= 0)
            {
                ulong currentValue = EnumHelper.ToUInt64(TypeCode, keys[index]);
                if (index == 0 && currentValue == 0)
                {
                    break;
                }

                if ((resultValue & currentValue) == currentValue)
                {
                    resultValue -= currentValue;
                    foundItems[foundItemsCount++] = index;
                    resultLength = checked(resultLength + names[index].Length);
                }

                index--;
            }
            while (--foundItemsCount >= 0)
            {
                yield return names[foundItems[foundItemsCount]];
            }
        }

        public string ConvertToString(TKey value, string separator)
        {
            var names = Values.ToArray();
            var keys = Keys.ToArray();
            ulong resultValue = EnumHelper.ToUInt64(TypeCode, value);

            if (resultValue == 0)
            {
                if (keys.Length > 0)
                {
                    ulong v = EnumHelper.ToUInt64(TypeCode, keys[0]);
                    if (v == 0)
                    {
                        return names[0];
                    }
                    return string.Empty;
                }
            }

            // Walk from largest to smallest. It's common to have a flags enum with a single
            // value that matches a single entry, in which case we can just return the existing
            // name string.
            int index = keys.Length - 1;
            while (index >= 0)
            {
                ulong cmpValue = EnumHelper.ToUInt64(TypeCode, keys[index]);
                if (cmpValue == resultValue)
                {
                    return names[index];
                }

                if (cmpValue < resultValue)
                {
                    break;
                }

                index--;
            }
            // With a ulong result value, regardless of the enum's base type, the maximum
            // possible number of consistent name/values we could have is 64, since every
            // value is made up of one or more bits, and when we see values and incorporate
            // their names, we effectively switch off those bits.
            Span<int> foundItems = stackalloc int[64];

            // Now look for multiple matches, storing the indices of the values
            // into our span.
            int resultLength = 0, foundItemsCount = 0;
            while (index >= 0)
            {
                ulong currentValue = EnumHelper.ToUInt64(TypeCode, keys[index]);
                if (index == 0 && currentValue == 0)
                {
                    break;
                }

                if ((resultValue & currentValue) == currentValue)
                {
                    resultValue -= currentValue;
                    foundItems[foundItemsCount++] = index;
                    resultLength = checked(resultLength + names[index].Length);
                }

                index--;
            }

            // If we exhausted looking through all the values and we still have
            // a non-zero result, we couldn't match the result to only named values.
            // In that case, we return null and let the call site just generate
            // a string for the integral value.
            if (resultValue != 0)
            {
                return string.Empty;
            }

            // We know what strings to concatenate.  Do so.

            Debug.Assert(foundItemsCount > 0);
            int length = checked(resultLength + (separator.Length * (foundItemsCount - 1)));
            Span<char> result = length > 64 ?
                (new char[length]).AsSpan() :
                stackalloc char[length];        // use stackalloc, making faster 
            Span<char> resultStart = result;    // i wanna use string.MakeFasterString Method...

            string name = names[foundItems[--foundItemsCount]];
            name.CopyTo(result);
            result = result[name.Length..];
            while (--foundItemsCount >= 0)
            {
                separator.CopyTo(result);
                result = result[separator.Length..];

                name = names[foundItems[foundItemsCount]];
                name.CopyTo(result);
                result = result[name.Length..];
            }
            Debug.Assert(result.IsEmpty);

            return new string(resultStart);
        }

        private static readonly TypeCode TypeCode = Type.GetTypeCode(typeof(TKey));

        private class EnumComparer : IComparer<TKey>
        {
            [MethodImpl(MethodImplOptions.AggressiveInlining)]
            public int Compare(TKey x, TKey y)
            {
                ulong xValue = EnumHelper.ToUInt64(TypeCode, x);
                ulong yValue = EnumHelper.ToUInt64(TypeCode, y);

                return xValue.CompareTo(yValue);
            }
        }
    }
}
