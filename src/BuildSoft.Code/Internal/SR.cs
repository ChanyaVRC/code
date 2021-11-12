using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Internal;

internal static class SR
{
    public static string GetParamName(this ParamName paramName)
        => paramName switch
        {
            ParamName.value => nameof(ParamName.value),
            ParamName.arg => nameof(ParamName.arg),
            ParamName.arg0 => nameof(ParamName.arg0),
            ParamName.arg1 => nameof(ParamName.arg1),
            ParamName.arg2 => nameof(ParamName.arg2),
            ParamName.arg3 => nameof(ParamName.arg3),
            ParamName.index => nameof(ParamName.index),
            ParamName.count => nameof(ParamName.count),
            ParamName.length => nameof(ParamName.length),
            ParamName.name => nameof(ParamName.name),
            ParamName.fullName => nameof(ParamName.fullName),
            _ => paramName.ToString(),
        };
}
