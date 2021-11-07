using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public abstract class CsMemberContent : CsContent, IModifiable
    {
        public CsIdentifier Identifier { get; }
        public CsType Type { get; }

        private List<string>? _modifiers;
        public IReadOnlyCollection<string> Modifiers
            => _modifiers ??= new List<string>();

        public string Header => !IsImmutableHeader || _header == null ? _header = CreateHeader() : _header;
        protected bool IsImmutableHeader { get; set; } = true;

        protected virtual string CreateHeader()
            => string.Join(' ', Modifiers.Append(Type.GetOptimizedName()).Append(Identifier.Value));

        private string? _header;

        public CsMemberContent(CsIdentifier identifier, CsType type, IEnumerable<string>? modifiers = null)
        {
            Identifier = identifier;
            Type = type;
            if (modifiers != null)
            {
                _modifiers = new(modifiers);
            }
        }
    }
}
