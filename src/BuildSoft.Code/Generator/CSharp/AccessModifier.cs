namespace BuildSoft.Code.Generator.CSharp
{
    [Flags]
    public enum AccessModifier
    {
        Public = 1,
        Private = 2,
        Protected = 4,
        Internal = 8,
        ProtectedInternal = Protected | Internal,
        PrivateProtected = Private | Protected,

        Mask = (1 << UsingBytes) - 1,
        UsingBytes = 4,
    }
}

