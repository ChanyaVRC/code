namespace BuildSoft.Code.Content.CSharp;

public /*record*/ class CsTypeName
{
    public CsIdentifier BaseName { get; }

    public string Base => BaseName.Value;

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

    internal string Concat(string value, bool isOptimized = true)
    {
        if (IsRef)
        {
            value = "ref " + value;
        }
        if (IsGenericType)
        {
            value += $"<{string.Join(", ", GenericTypes!.Select(x => isOptimized ? x.GetOptimizedName() : x.FullName))}>";
        }
        if (IsPointer)
        {
            value += '*';
        }
        if (IsArray)
        {
            value += string.Join(null, Ranks.Select(x => $"[{new string(',', x - 1)}]"));
        }
        return value;
    }
    public int[] Ranks { get; }
    public CsType[]? GenericTypes { get; }
    public CsType? ParentType { get; }

    public bool IsArray { get; }
    public bool IsGenericType { get; }
    public bool IsRef { get; }
    public bool IsPointer { get; }

    public CsTypeName(string value) : this(value.AsSpan())
    {

    }

    public CsTypeName(ReadOnlySpan<char> value)
    {
        if (value.Length <= 0)
        {
            throw new ArgumentException("Identifier must be at least one character.", nameof(value));
        }

        List<int> ranks = new();
        while (true)
        {
            switch (value[^1])
            {
                case '&':
                    IsRef = true;
                    value = value[..^1];
                    break;
                case '*':
                    IsPointer = true;
                    value = value[..^1];
                    break;
                case ']' when value[^2] == ']':
                    IsGenericType = true;
                    GenericTypes = ConvertToTypesForFullName(value.ToString());
                    value = value[..value.IndexOf('[')];

                    break;
                case ']' when value[^2] != ']':
                    IsArray = true;
                    int lastIndex = value.LastIndexOf('[');
                    ranks.Add(value[lastIndex..^1].ToArray().Count(x => x == ',') + 1);
                    value = value[..lastIndex];
                    break;
                case '>':
                    IsGenericType = true;
                    int startindex = value.IndexOf('<');
                    GenericTypes = ConvertToTypes(value.ToString());
                    value = value[..startindex];
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
                    goto Exit;
            }
        }
Exit:
        Ranks = ranks.ToArray();
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
