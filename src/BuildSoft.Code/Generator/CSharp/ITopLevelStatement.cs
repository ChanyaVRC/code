namespace BuildSoft.Code.Generator.CSharp
{
    public interface ITopLevelStatement : INamespaceStatement
    {
        void WriteUsingDirective(string @namespace, bool isGlobal = false);
        void WriteUsingDirective(string alias, string reference, bool isGlobal = false);
        Task WriteUsingDirectiveAsync(string @namespace, bool isGlobal = false);
        Task WriteUsingDirectiveAsync(string alias, string reference, bool isGlobal = false);
    }
}
