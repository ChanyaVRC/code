﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.CsGenerator.Content
{
    internal class CsPropertyContent : CsMemberContent
    {
        public CsPropertyContent(string identifier, string type) : base(identifier, type)
        {
        }

        public override IReadOnlyCollection<string> Attributes => throw new NotImplementedException();

        public override string ToCode(out int contentPosition, ref int indent)
        {
            throw new NotImplementedException();
        }
    }
}