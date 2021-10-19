using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.CsGenerator.Content
{
    internal abstract class CsUserDefinedTypeContent
        : CsContent, IAvailable<CsUserDefinedTypeContent>, IAvailable<CsMemberContent>
    {
        protected CsUserDefinedTypeContent()
        {

        }


        public void AddContent(CsUserDefinedTypeContent content)
        {
            throw new NotImplementedException();
        }

        public bool RemoveContent(CsUserDefinedTypeContent content)
        {
            throw new NotImplementedException();
        }

        public void AddContent(CsMemberContent content)
        {
            throw new NotImplementedException();
        }

        public bool RemoveContent(CsMemberContent content)
        {
            throw new NotImplementedException();
        }
    }
}
