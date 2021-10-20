using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.CsGenerator.Content
{
    public abstract class CsNoContentsContent : CsContent, INoContentsContent
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
        {
            string body = ToCode(indent);
            contentPosition = body.Length;
            return body;
        }
        public abstract string ToCode(int indent);
    }
}
