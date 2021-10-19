using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.CsGenerator.Content
{
    internal class CsMethodContent : CsMemberContent
    {
        private List<string>? _attributes;
        private List<CsArgumentDefinition>? _arguments;
        public override IReadOnlyCollection<string> Attributes
            => _attributes ??= new List<string>();
        public IList<CsArgumentDefinition> Arguments
            => _arguments ??= new List<CsArgumentDefinition>();

        public string ArgumentList => CreateArgumentList();
        public string CreateArgumentList()
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
            string type,
            IEnumerable<string> attributes = null!,
            IEnumerable<CsArgumentDefinition> arguments = null!)
            : base(identifier, type)
        {
            if (attributes != null)
            {
                _attributes = new List<string>(attributes);
            }
            if (arguments != null)
            {
                _arguments = new List<CsArgumentDefinition>(arguments);
            }
        }


        public override string ToCode(out int contentPosition, ref int indent)
        {
            string indentInstance = CreateIndent(indent);
            string code =
$@"{indentInstance}{Header}({ArgumentList})
{indentInstance}{{

{indentInstance}}}
";
            contentPosition = code.Length - "\r\n}\r\n".Length - indentInstance.Length;
            indent++;
            return code;
        }
    }
}
