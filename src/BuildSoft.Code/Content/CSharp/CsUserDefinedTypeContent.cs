using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public abstract class CsUserDefinedTypeContent
        : CsContent, IAvailable<CsUserDefinedTypeContent>, IAvailable<CsMemberContent>, IModifiable
    {
        public abstract string Keyword { get; }
        public CsIdentifier Identifier { get; }
        public IReadOnlyCollection<string> Modifiers => _modifiers ??= new List<string>();
        private List<string>? _modifiers;
        
        public string? SubClass { get; }
        public IReadOnlyCollection<string> SubInterfaces => _interfaces ??= new List<string>();
        private List<string>? _interfaces; 

        public string Header => !IsImmutableHeader || _header == null ? _header = CreateHeader() : _header;
        public string? _header;

        protected bool IsImmutableHeader { get; set; } = true;

        // TODO: Review of algorithm, this is very slow.
        protected virtual string CreateHeader()
        {
            IEnumerable<string> values = (_modifiers ?? Enumerable.Empty<string>())
                .Append(Keyword).Append(Identifier.Value);

            if (!string.IsNullOrEmpty(SubClass) || (_interfaces != null && _interfaces.Count > 0))
            {
                IEnumerable<string> bases = Enumerable.Empty<string>();
                if (!string.IsNullOrEmpty(SubClass))
                {
                    bases = bases.Append(SubClass);
                }
                if (_interfaces != null && _interfaces.Count > 0)
                {
                    bases = bases.Concat(_interfaces);
                }
                values = values.Append(":").Append(string.Join(", ", bases.Select(x => x.Trim())));
            }

            return string.Join(' ', values.Select(x => x.Trim()));
        }

        protected CsUserDefinedTypeContent(CsIdentifier identifier, IReadOnlyCollection<string>? modifiers = null, string? subClass = null, IReadOnlyCollection<string>? baseInterfaces = null)
        {
            Identifier = identifier;
            if (modifiers != null)
            {
                _modifiers = new(modifiers);
            }

            SubClass = subClass;
            if (baseInterfaces != null)
            {
                _interfaces = new(baseInterfaces);
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

        public override Code ToCode(string indent)
        {
            string body;
            if (indent.Length == 0)
            {
                body = $"{Header}\r\n{{\r\n}}\r\n";
            }
            else
            {
                body =
@$"{indent}{Header}
{indent}{{
{indent}}}
";
            }
            int contentsStartIndex = body.Length - (indent.Length + "}\r\n".Length);

            return Code.CreateCodeWithContents(body, contentsStartIndex, true);
        }
    }
}
