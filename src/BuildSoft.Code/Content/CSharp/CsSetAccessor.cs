using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp;

public sealed class CsSetAccessor : CsPropertyAccessor, IAvailable<CsStatementContent>
{
    public static readonly CsSetAccessor Auto = new(true);

    public override string Keyword => "set";
    
    public CsSetAccessor(bool isAuto = false) : base(isAuto)
    {
        
    }
}
