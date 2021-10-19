using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.CsGenerator.Content
{
    internal class CsFieldContent : CsMemberContent
    {
        private List<string>? _attributes;
        public override IReadOnlyCollection<string> Attributes
            => _attributes ??= new List<string>();

        public CsFieldContent(string identifier, string type, IEnumerable<string> attributes = null!) : base(identifier, type)
        {
            if (attributes != null)
            {
                _attributes = new List<string>(attributes);
            }
        }

        public string ToCode(int indent)
            => $"{CreateIndent(indent)}{Header};\r\n";

        public override string ToCode(out int contentPosition, ref int indent)
        {
            string code = ToCode(indent);
            contentPosition = code.Length;
            return code;
        }
    }
}
