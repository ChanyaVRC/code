namespace BuildSoft.Code.CsGenerator
{
    public interface INamespaceStatement : ICsStatement
    {
        public void WriteUsingDirective(string @namespace)
        {
            string content = $"using {@namespace};";
            Writer.AppendLine(content);
        }
    }
}
