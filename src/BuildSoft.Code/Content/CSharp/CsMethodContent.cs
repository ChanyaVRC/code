using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public class CsMethodContent : CsMemberContent
    {
        private List<CsArgumentDefinition>? _arguments;
        public IReadOnlyCollection<CsArgumentDefinition> Arguments
            => _arguments ??= new List<CsArgumentDefinition>();

        public string ArgumentList => CreateArgumentList();
        protected virtual string CreateArgumentList()
        {
            StringBuilder builder = new();
            foreach (var (type, identifier) in Arguments)
            {
                if (builder.Length != 0)
                {
                    builder.Append(',');
                    builder.Append(' ');
                }
                builder.Append(type.Trim());
                builder.Append(' ');
                builder.Append(identifier.Trim());
            }
            return builder.ToString();
        }

        public CsMethodContent(
            string identifier,
            string returnType,
            IEnumerable<string>? modifiers = null,
            IEnumerable<CsArgumentDefinition>? arguments = null)
            : base(identifier, returnType, modifiers)
        {
            if (arguments != null)
            {
                _arguments = new List<CsArgumentDefinition>(arguments);
            }
        }


        public override Code ToCode(string indent)
        {
            string code =
$@"{indent}{Header}({ArgumentList})
{indent}{{
{indent}}}
";
            int position = code.Length - "}\r\n".Length - indent.Length;
            return Code.CreateCodeWithContents(code, position, true);
        }
    }
}
