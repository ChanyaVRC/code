using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content
{
    public record class Code
    {
        public static readonly Code Empty = new("", 0, false, false); 

        public string Body { get; internal init; }
        public int ContentStartIndex { get; internal init; }
        public bool NeedsIndent { get; internal init; }
        public bool HasContents { get; internal init; }

        internal Code(string body, int contentPosition, bool needsIndent, bool hasContent)
        {
            Body = body;
            ContentStartIndex = contentPosition;
            NeedsIndent = needsIndent;
            HasContents = hasContent;
        }

        public static Code CreateCodeWithContents(string body, int contentPosition, bool needsIndent) 
            => new(body, contentPosition, needsIndent, true);
        public static Code CreateCodeWithNoContents(string body) 
            => new(body, body.Length, false, false);

        public void Deconstruct(out string body, out int contentPosition, out bool needsIndent, out bool hasContent)
        {
            body = Body;
            contentPosition = ContentStartIndex;
            needsIndent = NeedsIndent;
            hasContent = HasContents;
        }
    }
}
