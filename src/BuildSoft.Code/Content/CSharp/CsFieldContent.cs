﻿using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public class CsFieldContent : CsMemberContent
    {
        public CsFieldContent(string identifier, string type, IEnumerable<string>? modifiers = null) : base(identifier, type, modifiers)
        {
            CanOperateContents = false;
        }

        public override Code ToCode(string indent)
            => Code.CreateCodeWithNoContents($"{indent}{Header};\r\n");
    }
}