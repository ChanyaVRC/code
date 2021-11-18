using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp;

public class CsNamespaceContent : CsContent, IAvailable<CsNamespaceContent>, IAvailable<CsUserDefinedTypeContent>
{
    public CsNamespaceContent(CsNamespace @namespace)
    {
        Namespace = @namespace;
    }

    public CsNamespace Namespace { get; }

    public void AddContent(CsNamespaceContent content)
        => AddableContents.Add(content);

    public bool RemoveContent(CsNamespaceContent content)
        => AddableContents.Remove(content);

    public void AddContent(CsUserDefinedTypeContent content)
        => AddableContents.Add(content);

    public bool RemoveContent(CsUserDefinedTypeContent content)
        => AddableContents.Remove(content);

    public override Code ToCode(string indent)
    {
        string body;
        int contentPosition;

        if (indent.Length == 0)
        {
            body = $"namespace {Namespace}\r\n{{\r\n}}\r\n";
        }
        else
        {
            body =
                $"{indent}namespace {Namespace}\r\n"
                + $"{indent}{{\r\n"
                + $"{indent}}}\r\n";
        }
        contentPosition = body.Length - (indent.Length + "}\r\n".Length);

        return Code.CreateWithContents(body, contentPosition, true);
    }
}
