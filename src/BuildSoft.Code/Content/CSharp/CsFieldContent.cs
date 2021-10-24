using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public class CsFieldContent : CsMemberContent, INoContentsContent<CsContent>
    {
        private List<string>? _attributes;
        private CsContent[]? _emptyContent;

        public override IReadOnlyCollection<string> Modifiers
            => _attributes ??= new List<string>();
        public override IReadOnlyList<CsContent> Contents
            => _emptyContent ??= Array.Empty<CsContent>();

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
            => ContentHelper.ToCodeForNoContent(ToCode, out contentPosition, indent);

    }
}
