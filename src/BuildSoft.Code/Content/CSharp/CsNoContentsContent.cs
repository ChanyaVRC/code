using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public abstract class CsNoContentsContent : CsContent, INoContentsContent<CsContent>
    {
        public sealed override IReadOnlyList<CsContent> Contents
            => _contents ??= Array.Empty<CsContent>();
        private CsContent[]? _contents;

        public sealed override void AddContent(CsLineContent content)
            => throw new InvalidOperationException();
        public sealed override bool RemoveContent(CsLineContent content)
            => throw new InvalidOperationException();
        public sealed override void ClearContents()
            => throw new InvalidOperationException();

        public sealed override string ToCode(out int contentPosition, ref int indent)
            => ContentHelper.ToCodeForNoContent(ToCode, out contentPosition, indent);

        public abstract string ToCode(int indent);
    }
}
