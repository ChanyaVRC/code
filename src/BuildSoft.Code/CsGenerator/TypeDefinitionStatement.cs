using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Reflection;
using System.Diagnostics;
using BuildSoft.Code.Collection;

namespace BuildSoft.Code.CsGenerator
{
    public partial class TypeDefinitionStatement : CsStatement
    {
        private string _keyword;
        public override string HeadKeyword => _keyword;

        private static string CreateHead(TypeDefinitionModifierFlags flags, TypeDefinitionKeyword type, string name)
        {
            string[] array = new string[] { Modifier.GetTypeKeyword(type), name };
            return string.Join(' ', Modifier.GetModifiers(flags).Concat(array));
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

