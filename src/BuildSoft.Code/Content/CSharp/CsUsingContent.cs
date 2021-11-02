using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public class CsUsingContent : CsContent
    {
        public string Namespace { get; }
        private string CodeBody => _codeBodyCache ??= $"using {Namespace};\r\n";
        private string? _codeBodyCache;

        public CsUsingContent(string @namespace)
        {
            CanOperateContents = false;
            Namespace = @namespace;
        }

        public override Code ToCode(string indent) 
            => Code.CreateCodeWithNoContents(indent.Length == 0 ? CodeBody : indent + CodeBody);
    }
}
