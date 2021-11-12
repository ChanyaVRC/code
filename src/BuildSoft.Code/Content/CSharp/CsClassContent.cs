using System;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp;

public class CsClassContent : CsUserDefinedTypeContent
{
    public CsClassContent(CsTypeName name, IReadOnlyCollection<string>? modifiers = null, CsType? subClass = null, IReadOnlyCollection<CsType>? baseInterfaces = null)
        : base(name, modifiers, subClass, baseInterfaces)
    {

    }

    public override string Keyword => "class";
}
