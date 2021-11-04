namespace BuildSoft.Code.Content.CSharp
{
    public record CsArgumentDefinition(string Type, string Identifier)
    {
        public override string ToString()
        {
            return Type + ' ' + Identifier;
        }
    }
}
