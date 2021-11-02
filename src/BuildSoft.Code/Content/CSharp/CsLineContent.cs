using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public class CsLineContent : CsContent
    {
        public CsLineContent()
        {
            CanOperateContents = false;
        }
        public override Code ToCode(string indent) 
            => Code.CreateCodeWithNoContents("\r\n");
    }
}
