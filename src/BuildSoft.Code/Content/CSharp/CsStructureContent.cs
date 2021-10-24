using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public class CsStructureContent : CsUserDefinedTypeContent
    {
        public CsStructureContent(string identifier, IReadOnlyCollection<string> modifiers = null!, IReadOnlyCollection<string> baseInterfaces = null!)
            : base(identifier, modifiers, baseInterfaces: baseInterfaces)
        {
        }

        public override string Keyword => "struct";
    }
}
