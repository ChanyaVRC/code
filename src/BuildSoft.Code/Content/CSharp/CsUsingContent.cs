using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public class CsUsingContent : CsNoContentsContent
    {
        public string Namespace { get; }

        public CsUsingContent(string @namespace)
        {
            Namespace = @namespace;
        }

        public override string ToCode(int indent)
        {
            return $"{CreateIndent(indent)}using {Namespace};\r\n";
        }
    }
}
