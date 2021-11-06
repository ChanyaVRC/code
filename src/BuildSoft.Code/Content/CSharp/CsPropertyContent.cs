using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public class CsPropertyContent : CsMemberContent
    {
        public CsPropertyContent(CsIdentifier identifier, string type, IEnumerable<string>? modifiers = null) 
            : base(identifier, type, modifiers)
        {

        }

        public override Code ToCode(string indent)
        {
            string body;
            if (indent.Length == 0)
            {
                body = $"{Header}\r\n{{\r\n}}\r\n";
            }
            else
            {
                body =
@$"{indent}{Header}
{indent}{{
{indent}}}
";
            }
            int contentsStartIndex = body.Length - (indent.Length + "}\r\n".Length);
            return Code.CreateCodeWithContents(body, contentsStartIndex, true);
        }
    }
}
