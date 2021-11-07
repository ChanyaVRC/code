﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public class CsTopLevelContent 
        : CsContent, IAvailable<CsNamespaceContent>, IAvailable<CsUsingDirectiveContent>
    {
        private static readonly Code _code = Code.CreateCodeWithContents("", 0, false);
 
        public CsTopLevelContent()
        {
        }

        public override Code ToCode(string indent) => _code;

        public void AddContent(CsNamespaceContent content)
            => AddableContents.Add(content);
        public void AddContent(CsUsingDirectiveContent content)
            => AddableContents.Add(content);

        public bool RemoveContent(CsNamespaceContent content)
            => AddableContents.Remove(content);
        public bool RemoveContent(CsUsingDirectiveContent content)
            => AddableContents.Remove(content);
    }
}
