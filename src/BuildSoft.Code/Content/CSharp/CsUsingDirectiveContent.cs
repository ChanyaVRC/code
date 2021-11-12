using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp;

public class CsUsingDirectiveContent : CsContent
{
    public CsNamespace Namespace { get; }
    public bool IsGlobal { get; }
    private string CodeBody
    {
        get
        {
            if (_codeBodyCache == null)
            {
                if (IsGlobal)
                {
                    _codeBodyCache = $"global using {Namespace};\r\n";
                }
                else
                {
                    _codeBodyCache = $"using {Namespace};\r\n";
                }
            }

            return _codeBodyCache;
        }
    }
    private string? _codeBodyCache;

    public CsUsingDirectiveContent(CsNamespace @namespace, bool isGlobal = false)
    {
        CanOperateContents = false;
        Namespace = @namespace;
        IsGlobal = isGlobal;
    }

    public override Code ToCode(string indent)
        => Code.CreateCodeWithNoContents(indent.Length == 0 ? CodeBody : indent + CodeBody);
}
