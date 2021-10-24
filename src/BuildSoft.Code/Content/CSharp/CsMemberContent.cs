using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public abstract class CsMemberContent : CsContent, IModifiable
    {
        public string Identifier { get; }
        public string Type { get; }
        public abstract IReadOnlyCollection<string> Modifiers { get; }
        public virtual string Header => !IsImmutableHeader || _header == null ? _header = CreateHeader() : _header;
        protected bool IsImmutableHeader { get; set; } = true;

        private string CreateHeader()
            => string.Join(' ', Modifiers.Append(Type).Append(Identifier));

        public string? _header;

        public CsMemberContent(string identifier, string type)
        {
            Identifier = identifier;
            Type = type;
        }
    }
}
