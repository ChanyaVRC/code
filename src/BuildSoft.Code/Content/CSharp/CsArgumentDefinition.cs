namespace BuildSoft.Code.Content.CSharp
{
    public record CsArgumentDefinition(string Type, string Identifier, string? Modifier = null)
    {
        public override string ToString()
        {
            if (string.IsNullOrEmpty(Modifier))
            {
                return Type + ' ' + Identifier;
            }
            return Modifier + ' ' + Type + ' ' + Identifier;
        }
    }
}
