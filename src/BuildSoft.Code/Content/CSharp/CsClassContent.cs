using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public class CsClassContent : CsUserDefinedTypeContent
    {
        public CsClassContent(CsIdentifier identifier, IReadOnlyCollection<string>? modifiers = null, string? baseClass = null, IReadOnlyCollection<string>? baseInterfaces = null)
            : base(identifier, modifiers, baseClass, baseInterfaces)
        {
            
        }

        public override string Keyword => "class";
    }
}
