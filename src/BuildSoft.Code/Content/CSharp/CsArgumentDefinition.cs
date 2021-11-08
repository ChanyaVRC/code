namespace BuildSoft.Code.Content.CSharp
{
    public record CsArgumentDefinition(CsType Type, CsIdentifier Identifier, string? Modifier = null)
    {
        public string ToOptimizedString()
        {
            if (string.IsNullOrEmpty(Modifier))
            {
                return Type.GetOptimizedName() + ' ' + Identifier;
            }
            return Modifier + ' ' + Type.GetOptimizedName() + ' ' + Identifier;
        }
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Modifier))
            {
                return Type.FullName + ' ' + Identifier;
            }
            return Modifier + ' ' + Type.FullName + ' ' + Identifier;
        }
    }
}
