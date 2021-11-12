using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Generator.CSharp;

public class CsTopLevelStatement : CsStatement, IDisposable, IAsyncDisposable, ITopLevelStatement
{
    public override string HeadKeyword => string.Empty;

    internal CsTopLevelStatement(CsFileWriter writer) : base(writer)
    {

    }

    public NamespaceStatement CreateNamespaceStatement(string @namespace)
    {
        return new NamespaceStatement(@namespace, this);
    }

    public void WriteUsingDirective(string @namespace, bool isGlobal = false)
    {
        string content = $"using {@namespace};";
        if (isGlobal)
        {
            content = "global " + content;
        }
        Writer.AppendLine(content);
    }
    public void WriteUsingDirective(string alias, string reference, bool isGlobal = false)
    {
        string content = $"using {alias} = {reference};";
        if (isGlobal)
        {
            content = "global " + content;
        }
        Writer.AppendLine(content);
    }

    public async Task WriteUsingDirectiveAsync(string @namespace, bool isGlobal = false)
    {
        string content = $"using {@namespace};";
        if (isGlobal)
        {
            content = "global " + content;
        }
        await Writer.AppendLineAsync(content);
    }
    public async Task WriteUsingDirectiveAsync(string alias, string reference, bool isGlobal = false)
    {
        string content = $"using {alias} = {reference};";
        if (isGlobal)
        {
            content = "global " + content;
        }
        await Writer.AppendLineAsync(content);
    }

}
