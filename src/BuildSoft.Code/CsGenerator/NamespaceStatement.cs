using System.Diagnostics.CodeAnalysis;

namespace BuildSoft.Code.CsGenerator
{
    public class NamespaceStatement : CsStatement, INamespaceStatement
    {
        private const string Keyword = "namespace";
        public override string HeadKeyword => Keyword;

        public string Namespace { get; }
        
        protected internal NamespaceStatement(string @namespace, CsStatement parent) : base($"{Keyword} {@namespace}", parent)
        {
            Namespace = @namespace;
            CurrentNamespace = parent.CurrentNamespace + "." + @namespace;
        }

        public NamespaceStatement CreateNamespaceStatement(string @namespace)
        {
            return new NamespaceStatement(@namespace, this);
        }

        public TypeDefinitionStatement CreateClassStatement(
            string name,
            AccessModifier accessModifier = AccessModifier.Public,
            TypeAttributeFlags attribute = TypeAttributeFlags.None)
        {
            return CreateTypeStatement(TypeDefinitionKeyword.Class, name, accessModifier, attribute);
        }
        public TypeDefinitionStatement CreateStructureStatement(
            string name,
            AccessModifier accessModifier = AccessModifier.Public,
            TypeAttributeFlags attribute = TypeAttributeFlags.None)
        {
            return CreateTypeStatement(TypeDefinitionKeyword.Structure, name, accessModifier, attribute);
        }

        public TypeDefinitionStatement CreateRecordStatement(
            string name,
            AccessModifier accessModifier = AccessModifier.Public,
            TypeAttributeFlags attribute = TypeAttributeFlags.None)
        {
            return CreateTypeStatement(TypeDefinitionKeyword.Record, name, accessModifier, attribute);
        }

        public TypeDefinitionStatement CreateTypeStatement(
            TypeDefinitionKeyword keyword,
            string name,
            AccessModifier accessModifier = AccessModifier.Public,
            TypeAttributeFlags attribute = TypeAttributeFlags.None)
        {
            return new(TypeDefinitionModifier.Union(accessModifier, attribute), keyword, name, this);
        }

        public void WriteUsingDirective(string @namespace)
        {
            string content = $"using {@namespace};";
            Writer.AppendLine(content);
        }
    }
}