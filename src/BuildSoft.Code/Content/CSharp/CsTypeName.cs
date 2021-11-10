namespace BuildSoft.Code.Content.CSharp
{
    public /*record*/ class CsTypeName
    {
        private readonly CsIdentifier _baseType;
        public string Value
        {
            get
            {
                string result = _baseType.Value;
                var genericTypes = GenericTypes;
                if (genericTypes != null)
                {
                    result += "<" + string.Join(", ", genericTypes.Select(x => x.GetOptimizedName())) + ">";
                }
                if (IsArray)
                {
                    result += $"[{new string(',', Rank - 1)}]";
                }
                return result;
            }
        }
        public bool IsArray { get; }
        public int Rank { get; }
        public CsType[]? GenericTypes { get; }
        public bool IsGenericType { get; }
        public bool IsRef { get;  }
        public CsTypeName? ParentType { get; }

        public CsTypeName(string value)
        {
            if (value.Length <= 0)
            {
                throw new ArgumentException("Identifier must be at least one character.", nameof(value));
            }

            string baseTypeString = value;
            if (baseTypeString[^1] == '&')
            {
                baseTypeString = baseTypeString[..^1];
                IsRef = true;
            }
            if (baseTypeString[^1] == '*')
            {
                baseTypeString = baseTypeString[..^1];
                IsRef = true;
            }

            if (baseTypeString[^1] == '>')
            {
                int typesStartIndex = value.IndexOf('<');
                GenericTypes = ConvertToTypes(baseTypeString);
                IsGenericType = true;
                baseTypeString = baseTypeString[..typesStartIndex];
            }
            else if (baseTypeString[^1] == ']')
            {
                int startIndex = value.IndexOf('[');
                if (baseTypeString[^2] == ']')
                {
                    GenericTypes = ConvertToTypesForFullName(baseTypeString);
                    IsGenericType = true;
                    baseTypeString = baseTypeString[..startIndex];
                }
                else
                {
                    Rank = baseTypeString[startIndex..].Count(x => x == ',') + 1;
                    baseTypeString = baseTypeString[..startIndex];
                    IsArray = true;
                }
            }
            if (baseTypeString.Contains('`'))
            {
                baseTypeString = baseTypeString[..value.IndexOf('`')];
                IsGenericType = true;
            }

            int parentTypeIndex = baseTypeString.LastIndexOf('+');
            if (parentTypeIndex >= 0)
            {
                string source = baseTypeString;
                baseTypeString = source[(parentTypeIndex + 1)..];
                ParentType = source[..parentTypeIndex];
            }
            _baseType = baseTypeString;
        }

        private static CsType[] ConvertToTypes(string typesString)
        {
            List<CsType> list = new();
            int startIndex = typesString.IndexOf('<');

            if (startIndex < 0)
            {
                return Array.Empty<CsType>();
            }

            if (typesString[^1] != '>')
            {
                throw new ArgumentException();
            }

            int nest = 0;
            ReadOnlySpan<char> span = typesString.AsSpan()[(startIndex + 1)..^1];
            int currentIndex = 0;
            while (true)
            {
                switch (span[currentIndex])
                {
                    case ',' when nest == 0:
                        list.Add(new CsType(span[..(currentIndex)].ToString().Trim()));
                        span = span[(currentIndex + 1)..];
                        currentIndex = 0;
                        break;
                    case '<':
                        nest++;
                        goto default;
                    case '>' when nest > 0:
                        nest--;
                        goto default;
                    default:
                        currentIndex++;
                        break;
                }
                if (span.Length == currentIndex)
                {
                    if (nest != 0)
                    {
                        throw new ArgumentException();
                    }

                    if (!span.IsEmpty)
                    {
                        list.Add(new CsType(span.ToString().Trim()));
                    }
                    break;
                }
            }
            return list.ToArray();
        }
        private static CsType[] ConvertToTypesForFullName(string typesString)
        {
            List<CsType> list = new();
            int startIndex = typesString.IndexOf("[[");

            if (startIndex < 0)
            {
                return Array.Empty<CsType>();
            }

            if (!typesString.EndsWith("]]"))
            {
                throw new ArgumentException();
            }

            int nest = 0;
            ReadOnlySpan<char> span = typesString.AsSpan()[(startIndex + 1)..^1];
            int currentIndex = 0;
            bool isEnable = true;
            while (true)
            {
                switch (span[currentIndex])
                {
                    case ',' when nest == 0:
                        isEnable = true;
                        span = span[(currentIndex + 1)..];
                        currentIndex = 0;
                        break;
                    case ']' when nest == 1 && isEnable:
                    case ',' when nest == 1 && isEnable:
                        list.Add(new CsType(span[1..currentIndex].ToString().Trim()));
                        if (span[currentIndex] == ',')
                        {
                            isEnable = false;
                        }
                        span = span[(currentIndex + 1)..];
                        currentIndex = 0;
                        break;
                    case '[':
                        nest++;
                        goto default;
                    case ']' when nest > 0:
                        nest--;
                        goto default;
                    default:
                        currentIndex++;
                        break;
                }
                if (span.Length == currentIndex)
                {
                    if (nest != 0)
                    {
                        throw new ArgumentException();
                    }
                    break;
                }
            }
            return list.ToArray();
        }

        public override int GetHashCode()
        {
            return Value == null ? 0 : Value.GetHashCode();
        }

        public override bool Equals(object? obj)
        {
            if (obj is not null && obj is CsTypeName @namespace)
            {
                return Equals(@namespace);
            }
            return false;
        }
        public virtual bool Equals(CsTypeName? obj)
        {
            if (obj is null || GetType() != obj.GetType())
            {
                return false;
            }

            return Value == obj.Value;
        }

        public override string ToString() => Value;

        public static implicit operator CsTypeName(string value) => new(value);

        public static bool operator ==(CsTypeName? left, CsTypeName? right)
            => left is null ? right is null : left.Equals(right);
        public static bool operator !=(CsTypeName? left, CsTypeName? right) => !(left == right);
    }
}
