using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code
{
    internal static class CodeHelper
    {
        private const int DefaultTabSize = 4;
        private const int DefaultCapacity = DefaultTabSize * 10 + 1;

        internal static int TabSize { get; set; } = DefaultTabSize;

        private static string?[] _spaceCacheArray = Array.Empty<string?>();

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string CreateOrGetIndent(int indent)
        {
            return CreateOrGetIndentBySpaceCount(checked(indent * TabSize));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static string CreateOrGetIndentBySpaceCount(int count)
        {
            ThrowHelper.ThrowArgumentOutOfRangeExceptionIfNegative(count, ParamName.count);
            if (count == 0)
            {
                return string.Empty;
            }

            var cache = _spaceCacheArray;
            if (cache.Length >= count)
            {
                string? value = cache[count - 1];
                if (value != null)
                {
                    return value;
                }
            }
            else
            {
                cache = Grow(cache, count);
            }

            string indentContent = new(' ', count);
            cache[count - 1] = indentContent;
            _spaceCacheArray = cache;

            return indentContent;
        }

        private static string?[] Grow(string?[] cache, int min)
        {
            Debug.Assert(cache.Length < min);

            int newSize = cache.Length == 0 ? DefaultCapacity : 2 * cache.Length;

            if ((uint)newSize > Array.MaxLength)
            {
                newSize = Array.MaxLength;
            }

            if (newSize < min)
            {
                newSize = min;
            }

            string?[] newCache = new string?[newSize];
            if (cache.Length > 0)
            {
                Array.Copy(cache, newCache, cache.Length);
            }
            return newCache;
        }
    }
}
