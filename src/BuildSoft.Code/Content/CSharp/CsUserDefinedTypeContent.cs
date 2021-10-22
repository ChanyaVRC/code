using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    internal abstract class CsUserDefinedTypeContent
        : CsContent, IAvailable<CsUserDefinedTypeContent>, IAvailable<CsMemberContent>, IModifiable
    {
        public string Identifier { get; }
        public IReadOnlyCollection<string> Modifiers => _modifiers ??= new List<string>();
        private List<string>? _modifiers;

        protected CsUserDefinedTypeContent(string identifier, IReadOnlyCollection<string> modifiers = null!)
        {
            Identifier = identifier;
            if (modifiers != null)
            {
                _modifiers = new(modifiers);
            }
        }


        public void AddContent(CsUserDefinedTypeContent content)
            => AddableContents.Add(content);
        public bool RemoveContent(CsUserDefinedTypeContent content)
            => AddableContents.Remove(content);

        public void AddContent(CsMemberContent content)
            => AddableContents.Add(content);
        public bool RemoveContent(CsMemberContent content)
            => AddableContents.Remove(content);
    }
}
