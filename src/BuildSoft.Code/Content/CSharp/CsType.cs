using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    // TODO: Correspond to Generics
    public record class CsType
    {
        public CsNamespace Namespace { get; }
        public CsIdentifier Name { get; }
        public bool IsGeneric { get; }
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

        // TODO: Return the best name based on using directive.
        public string GetOptimizedName() => FullName switch
        {
            "System.SByte" => "sbyte",
            "System.Byte" => "byte",
            "System.Int16" => "short",
            "System.UInt16" => "ushort",
            "System.Int32" => "int",
            "System.UInt32" => "uint",
            "System.Int64" => "long",
            "System.UInt64" => "ulong",
            "System.Decimal" => "decimal",
            "System.Single" => "float",
            "System.Double" => "double",
            "System.Char" => "char",
            "System.Boolean" => "bool",
            "System.IntPtr" => "nint",
            "System.UIntPtr" => "nuint",
            "System.String" => "string",
            "System.Object" => "object",
            "System.Void" => "void",
            _ => FullName
        };

        public CsType(Type type)
            : this(type.FullName ?? type.Name, type.IsGenericType)
        {
        }

        public CsType(CsNamespace @namespace, CsIdentifier identifier)
        {
            Namespace = @namespace;
            Name = identifier;
        }

        public CsType(CsIdentifier identifier, bool isGeneric = false)
        {
            IsGeneric = isGeneric;
            Namespace = CsNamespace.Global;
            Name = identifier;
        }

        public CsType(string fullName, bool isGeneric = false)
        {
            IsGeneric = isGeneric;

            if (isGeneric)
            {
                Namespace = CsNamespace.Global;
                Name = new CsIdentifier(fullName);
            }
            else
            {
                if (fullName.StartsWith("global::"))
                {
                    fullName = fullName["global::".Length..];
                }
                int lastIndex = fullName.LastIndexOf('.');
                if (lastIndex == 0)
                {
                    ThrowHelper.ThrowArgumentException(null, ParamName.fullName);
                }

                if (lastIndex < 0)
                {
                    Namespace = CsNamespace.Global;
                    Name = new CsIdentifier(fullName);
                }
                else
                {
                    Namespace = new CsNamespace(fullName[..lastIndex]);
                    Name = new CsIdentifier(fullName[(lastIndex + 1)..]);
                }
            }
        }

        public override string ToString() => FullName;

        public static implicit operator CsType(string value) => new(value);
    }

}
