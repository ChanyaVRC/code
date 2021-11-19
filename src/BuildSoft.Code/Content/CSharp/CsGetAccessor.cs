using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp;

public sealed class CsGetAccessor : CsPropertyAccessor, IAvailable<CsStatementContent>
{
    public static readonly CsGetAccessor Auto = new(true);

    public override string Keyword => "get";
    
    public CsGetAccessor(bool isAuto = false) : base(isAuto)
    {
        
    }
}
