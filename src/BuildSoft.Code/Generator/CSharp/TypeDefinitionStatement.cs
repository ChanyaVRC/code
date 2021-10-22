using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using BuildSoft.Code.Collection;

namespace BuildSoft.Code.Generator.CSharp
{
    public partial class TypeDefinitionStatement : CsStatement
    {
        private readonly string _keyword;
        public override string HeadKeyword => _keyword;

        private static string CreateHead(TypeDefinitionModifierFlags flags, TypeDefinitionKeyword type, string name)
        {
            return string.Join(' ', Modifier.GetModifiers(flags).Append(Modifier.GetTypeKeyword(type)).Append(name));
        }

        protected internal TypeDefinitionStatement(
            TypeDefinitionModifierFlags flags, TypeDefinitionKeyword type, string name, CsStatement parent)
            : base(CreateHead(flags, type, name), parent)
        {
            _keyword = Modifier.GetTypeKeyword(type);
        }

        private protected TypeDefinitionStatement(string head, TypeDefinitionKeyword type, CsStatement parent) 
            : base(head, parent)
        {
            _keyword = Modifier.GetTypeKeyword(type);
        }

    }
}

