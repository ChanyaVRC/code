using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    internal class CsStructureContent : CsUserDefinedTypeContent
    {
        public CsStructureContent(string identifier, IReadOnlyCollection<string> modifiers) : base(identifier, modifiers)
        {
        }

        public override string ToCode(out int contentPosition, ref int indent)
        {
            throw new NotImplementedException();
        }
    }
}
