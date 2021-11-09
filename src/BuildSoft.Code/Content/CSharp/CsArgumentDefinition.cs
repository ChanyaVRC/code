namespace BuildSoft.Code.Content.CSharp
{
    public record CsArgumentDefinition(CsType Type, CsIdentifier Identifier, string? Modifier = null)
    {
        public string ToOptimizedString()
        {
            string result = Type.GetOptimizedName() + ' ' + Identifier;
            if (!string.IsNullOrEmpty(Modifier))
            {
                result = Modifier + ' ' + result;
            }
            return result;
        }

        public override string ToString()
        {
            string result = Type.GetOptimizedName() + ' ' + Identifier;
            if (!string.IsNullOrEmpty(Modifier))
            {
                result = Modifier + ' ' + result;
            }
            return result;
        }
    }
}
