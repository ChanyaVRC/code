using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public class CsLineContent : CsNoContentsContent
    {
        public override string ToCode(int indent)
        {
            return "\r\n";
        }
    }
}
