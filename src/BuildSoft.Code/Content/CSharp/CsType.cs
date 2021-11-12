using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    // TODO: Correspond to Generics
    public record class CsType
    {
        public CsNamespace Namespace { get; }
        public CsTypeName Name { get; }
        public bool IsGeneric { get; }
        public bool IsArray { get; }
        public string Value => Name.Value;
        public string FullName
        {
            get
            {
                string? namespaceString = Namespace.Value;
                if (namespaceString == null)
                {
                    return Name.Value;
                }
                return namespaceString + "." + Name.Value;
            }
        }

        public string BaseType
        {
            get
            {
                string? namespaceString = Namespace.Value;
                if (namespaceString == null)
                {
                    return Name.Base;
                }
                return namespaceString + "." + Name.Base;
            }
        }

        // TODO: Return the best name based on using directive.
        public string GetOptimizedName() => Name.Concat(GetOptimizedBaseTypeName());
        public string GetOptimizedBaseTypeName()
        {
            string baseType = BaseType;
            if (baseType.IsFullNameOfKeywordType())
            {
                return baseType.ToKeywordType();
            }
            return baseType;
        }

        public CsType(Type type)
            : this(type.FullName ?? type.Name)
        {
        }

        public CsType(CsNamespace @namespace, CsTypeName name)
        {
            Namespace = @namespace;
            Name = name;
        }

        public CsType(CsTypeName identifier)
        {
            Namespace = CsNamespace.Global;
            Name = identifier;
        }

        internal CsType(ReadOnlySpan<char> fullName) : this(fullName.ToString())
        {

        }
        public CsType(string fullName)
        {
            if (fullName.IsKeywordType())
            {
                fullName = fullName.ToFullname();
            }

            if (fullName.StartsWith("global::"))
            {
                fullName = fullName["global::".Length..];
            }

            string name = fullName;
            int index = name.IndexOf("[[");
            if (index >= 0)
            {
                name = fullName[..index];
            }
            else
            {
                index = name.IndexOf("<");
                if (index >= 0)
                {
                    name = fullName[..index];
                }
            }


            int lastIndex = name.LastIndexOf('.');
            if (lastIndex == 0)
            {
                ThrowHelper.ThrowArgumentException(null, ParamName.fullName);
            }

            if (lastIndex < 0)
            {
                Namespace = CsNamespace.Global;
                Name = new CsTypeName(fullName);
            }
            else
            {
                Namespace = new CsNamespace(fullName[..lastIndex]);
                Name = new CsTypeName(fullName[(lastIndex + 1)..]);
            }
        }

        public override string ToString() => FullName;

        public static implicit operator CsType(string value) => new(value);
        public static implicit operator CsType(Type value) => new(value);
    }
}
