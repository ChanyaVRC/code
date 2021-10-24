using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content
{
    internal class ContentHelper
    {
        public static string ToCodeForNoContent(Func<int, string> toCode, out int contentPosition, int indent)
        {
            string code = toCode(indent);
            contentPosition = code.Length;
            return code;
        }

    }
}
