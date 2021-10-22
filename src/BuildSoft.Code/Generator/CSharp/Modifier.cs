using BuildSoft.Code.Collection;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Generator.CSharp
{
    internal class Modifier
    {
        #region Master map
        private static readonly EnumPairs<AccessModifier> _accessModifierGenerator = new()
        {
            [AccessModifier.Public] = "public",
            [AccessModifier.Private] = "private",
            [AccessModifier.Protected] = "protected",
            [AccessModifier.Internal] = "internal",
        };

        private static readonly EnumPairs<TypeAttributeFlags> _typeAttributeGenerator = new()
        {
            [TypeAttributeFlags.New] = "new",
            [TypeAttributeFlags.Abstract] = "abstract",
            [TypeAttributeFlags.Sealed] = "sealed",
            [TypeAttributeFlags.Static] = "static",
            [TypeAttributeFlags.Partial] = "partial",
        };

        private static readonly Dictionary<TypeDefinitionKeyword, string> _keywordConverter = new()
        {
            [TypeDefinitionKeyword.Class] = "class",
            [TypeDefinitionKeyword.Structure] = "struct",
            [TypeDefinitionKeyword.Record] = "record",
            [TypeDefinitionKeyword.RecordClass] = "record class",
            [TypeDefinitionKeyword.RecordStructure] = "record struct",
        };
        #endregion

        public static IEnumerable< string> GetAccessModifiers(AccessModifier accessModifier)
        {
            return _accessModifierGenerator.GetStrings(accessModifier);
        }
        public static string GetAccessModifiersString(AccessModifier accessModifier)
        {
            // TODO: Use cache for making be faster
            return _accessModifierGenerator.ConvertToString(accessModifier, " ");
        }
        public static IEnumerable<string> GetTypeAttributes(TypeAttributeFlags accessModifier)
        {
            return _typeAttributeGenerator.GetStrings(accessModifier);
        }

        public static string GetTypeAttributesString(TypeAttributeFlags accessModifier)
        {
            // TODO: Use cache for making be faster
            return _typeAttributeGenerator.ConvertToString(accessModifier, " ");
        }
        public static string GetTypeKeyword(TypeDefinitionKeyword accessModifier)
        {
            return _keywordConverter[accessModifier];
        }

        public static IEnumerable<string> GetModifiers(TypeDefinitionModifierFlags flags)
        {
            var (accessModifier, typeAttribute) = flags;
            return GetAccessModifiers(accessModifier).Concat(GetTypeAttributes(typeAttribute));
        }

        public static string GetModifiersString(TypeDefinitionModifierFlags flags)
        {
            var (accessModifier, typeAttribute) = flags;
            return $"{GetAccessModifiersString(accessModifier)} {GetTypeAttributesString(typeAttribute)}".Trim();
        }
         
    }
}
