using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public class CsStructureContent : CsUserDefinedTypeContent
    {
        public CsStructureContent(CsIdentifier identifier, IReadOnlyCollection<string>? modifiers = null, IReadOnlyCollection<CsType>? baseInterfaces = null)
            : base(identifier, modifiers, baseInterfaces: baseInterfaces)
        {
        }

        public override string Keyword => "struct";
    }
}
