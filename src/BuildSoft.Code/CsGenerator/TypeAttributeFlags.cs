namespace BuildSoft.Code.CsGenerator
{
    [Flags]
    public enum TypeAttributeFlags
    {
        None = 0,
        New = 1,
        Abstract = 2,
        Sealed = 4,
        Static = 8,
        Partial = 16,

        Mask = (1 << UsingBytes) - 1,
        UsingBytes = 5,
    }
}

