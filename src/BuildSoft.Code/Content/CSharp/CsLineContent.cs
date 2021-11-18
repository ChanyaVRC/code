using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp;

public class CsLineContent : CsContent
{
    private static readonly Code _code = Code.CreateWithNoContents("\r\n");
    public CsLineContent()
    {
        CanOperateContents = false;
    }
    public override Code ToCode(string indent) => _code;
}
