namespace BuildSoft.Code.Content.CSharp
{

    public /*record*/ class CsTypeName
    {
        public CsType? BaseType { get; }
        public CsIdentifier? BaseName { get; }

        public string Base => BaseType?.Value ?? BaseName!.Value;
        public string OptimizedBase => BaseType?.GetOptimizedName() ?? BaseName!.Value;

        public string Value
        {
            get
            {
                return Concat(Base, false);
            }
        }

        public string OptimizedValue
        {
            get
            {
                return Concat(Base);
            }
        }

        internal string Concat(string value, bool isOptimized = true) => _type switch
        {
            TypeType.Pointer => value + '*',
            TypeType.Ref => "ref " + value,
            TypeType.Array => $"{value}[{new string(',', Rank - 1)}]",
            TypeType.Generic => $"{value}<{string.Join(", ", GenericTypes!.Select(x => isOptimized ? x.GetOptimizedName() : x.FullName))}>",
            _ => value,
        };
        public int Rank { get; }
        public CsType[]? GenericTypes { get; }
        public CsType? ParentType { get; }

        public bool IsArray => _type == TypeType.Array;
        public bool IsGenericType => _type == TypeType.Generic;
        public bool IsRef => _type == TypeType.Ref;
        public bool IsPointer => _type == TypeType.Pointer;

        private readonly TypeType _type;
        enum TypeType
        {
            Nomal, Ref, Pointer, Array, Generic,
        }

        public CsTypeName(string value) : this(value.AsSpan())
        {

        }

        public CsTypeName(ReadOnlySpan<char> value)
        {
            if (value.Length <= 0)
            {
                throw new ArgumentException("Identifier must be at least one character.", nameof(value));
            }

            switch (value[^1])
            {
                case '&':
                    _type = TypeType.Ref;
                    BaseType = new CsType(value[..^1]);
                    break;
                case '*':
                    _type = TypeType.Pointer;
                    BaseType = new CsType(value[..^1]);
                    break;
                case ']' when value[^2] == ']':
                    _type = TypeType.Generic;
                    GenericTypes = ConvertToTypesForFullName(value.ToString());
                    BaseType = new CsType(value[..value.IndexOf('[')]);

                    break;
                case ']' when value[^2] != ']':
                    _type = TypeType.Array;
                    int lastIndex = value.LastIndexOf('[');
                    Rank = value[lastIndex..^1].ToArray().Count(x => x == ',') + 1;
                    BaseType = new CsType(value[..lastIndex]);
                    break;
                case '>':
                    _type = TypeType.Generic;
                    int startindex = value.IndexOf('<');
                    GenericTypes = ConvertToTypes(value.ToString());
                    BaseType = new CsType(value[..startindex]);
                    break;
                default:
                    int last = value.LastIndexOf('+');
                    if (last >= 0)
                    {
                        ParentType = new CsType(value[..last]);
                        value = value[(last + 1)..];
                    }

                    last = value.LastIndexOf('`');
                    if (last >= 0)
                    {
                        value = value[..last];
                    }
                    BaseName = value.ToString();
                    break;
            }
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
