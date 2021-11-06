using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Internal
{
    internal static class SR
    {
        public static string GetParamName(this ParamName paramName)
            => paramName switch
            {
                ParamName.value => "value",
                ParamName.arg => "arg",
                ParamName.arg0 => "arg0",
                ParamName.arg1 => "arg1",
                ParamName.arg2 => "arg2",
                ParamName.arg3 => "arg3",
                ParamName.index => "index",
                ParamName.count => "count",
                ParamName.length => "length",
                _ => paramName.ToString(),
            };
    }
}
