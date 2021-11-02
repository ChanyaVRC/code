using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public class CsFileContent 
        : CsContent, IAvailable<CsNamespaceContent>, IAvailable<CsUsingContent>
    {
        public CsFileContent()
        {
        }

        public override Code ToCode(string indent)
        {
            return Code.CreateCodeWithContents("", 0, false);
        }

        public void AddContent(CsNamespaceContent content)
            => AddableContents.Add(content);
        public void AddContent(CsUsingContent content)
            => AddableContents.Add(content);

        public bool RemoveContent(CsNamespaceContent content)
            => AddableContents.Remove(content);
        public bool RemoveContent(CsUsingContent content)
            => AddableContents.Remove(content);
    }
}
