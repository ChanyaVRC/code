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
        public CsTypeName Name { get; }
        public IReadOnlyCollection<string> Modifiers => _modifiers ??= new List<string>();
        private List<string>? _modifiers;

        public CsType? SubClass { get; }
        public IReadOnlyCollection<CsType> SubInterfaces => _interfaces ??= new List<CsType>();
        private List<CsType>? _interfaces;

        public string Header => !IsImmutableHeader || _header == null ? _header = CreateHeader() : _header;
        private string? _header;

        protected bool IsImmutableHeader { get; set; } = true;

        // TODO: Review of algorithm, this is very slow.
        protected virtual string CreateHeader()
        {
            IEnumerable<string> values = (_modifiers ?? Enumerable.Empty<string>())
                .Append(Keyword).Append(Name.Value);

            if (SubClass != null || (_interfaces != null && _interfaces.Count > 0))
            {
                IEnumerable<CsType> bases = Enumerable.Empty<CsType>();
                if (SubClass != null)
                {
                    bases = bases.Append(SubClass);
                }
                if (_interfaces != null && _interfaces.Count > 0)
                {
                    bases = bases.Concat(_interfaces);
                }
                values = values.Append(":").Append(string.Join(", ", bases.Select(x => x.GetOptimizedName())));
            }

            return string.Join(' ', values.Select(x => x.Trim()));
        }

        protected CsUserDefinedTypeContent(CsTypeName name, IReadOnlyCollection<string>? modifiers = null, CsType? subClass = null, IReadOnlyCollection<CsType>? baseInterfaces = null)
        {
            Name = name;
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
