﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.CsGenerator.Content
{
    public class CsLineContent : CsNoContentsContent
    {
        public override string ToCode(int indent)
        {
            return "\r\n";
        }
    }
}
