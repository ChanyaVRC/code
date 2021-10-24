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
        public string Identifier { get; }
        public IReadOnlyCollection<string> Modifiers => _modifiers ??= new List<string>();
        private List<string>? _modifiers;
        
        public virtual string BaseClass { get; }
        public IReadOnlyCollection<string> BaseInterfaces => _interfaces ??= new List<string>();
        private List<string>? _interfaces;

        public virtual string Header => !IsImmutableHeader || _header == null ? _header = CreateHeader() : _header;
        public string? _header;

        protected bool IsImmutableHeader { get; set; } = true;

        // TODO: Review of algorithm, very slow.
        private string CreateHeader()
        {
            IEnumerable<string> values = Modifiers.Append(Keyword).Append(Identifier);
            if (string.IsNullOrEmpty(BaseClass) || BaseInterfaces.Count > 0)
            {
                values = values.Append(":");

                IEnumerable<string> bases = Enumerable.Empty<string>();
                if (string.IsNullOrEmpty(BaseClass))
                {
                    bases = bases.Append(BaseClass);
                }
                if (BaseInterfaces.Count > 0)
                {
                    bases = bases.Concat(BaseInterfaces);
                }
                values = values.Append(string.Join(", ", bases.Select(x => x.Trim())));
            }

            return string.Join(' ', values.Select(x => x.Trim()));
        }

        protected CsUserDefinedTypeContent(string identifier, IReadOnlyCollection<string> modifiers = null!, string baseClass = "",IReadOnlyCollection<string> baseInterfaces = null!)
        {
            Identifier = identifier;
            if (modifiers != null)
            {
                _modifiers = new(modifiers);
            }

            BaseClass = baseClass;
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

        public override string ToCode(out int contentPosition, ref int indent)
        {
            string indentInstance = CreateIndent(indent);

            string content =
@$"{indentInstance}{Header}
{indentInstance}{{

{indentInstance}}}
";
            contentPosition = content.Length - (indentInstance.Length + "\r\n}\r\n".Length);
            indent++;

            return content;
        }
    }
}
