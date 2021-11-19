using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp;

public sealed class CsInitAccessor : CsPropertyAccessor, IAvailable<CsStatementContent>
{
    public static readonly CsInitAccessor Auto = new();

    public override string Keyword => "init";
    
    public CsInitAccessor() : base(true)
    {
        
    }
}
