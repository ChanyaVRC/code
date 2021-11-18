using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content;

public record class Code
{
    public static readonly Code Empty = new(string.Empty, 0, false, false);

    public string Body { get; internal init; }
    public int ContentsStartIndex { get; internal init; }
    public bool NeedsIndent { get; internal init; }
    public bool HasContents { get; internal init; }

    internal Code(string body, int contentsStartIndex, bool needsIndent, bool hasContent)
    {
        Body = body;
        ContentsStartIndex = contentsStartIndex;
        NeedsIndent = needsIndent;
        HasContents = hasContent;
    }

    public static Code CreateWithContents(string body, int contentsStartIndex, bool needsIndent)
        => new(body, contentsStartIndex, needsIndent, true);
    public static Code CreateWithNoContents(string body)
        => new(body, body.Length, false, false);

    public void Deconstruct(out string body, out int contentsStartIndex, out bool needsIndent, out bool hasContent)
    {
        body = Body;
        contentsStartIndex = ContentsStartIndex;
        needsIndent = NeedsIndent;
        hasContent = HasContents;
    }
}
