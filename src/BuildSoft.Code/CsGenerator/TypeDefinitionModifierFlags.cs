using System.Runtime.CompilerServices;

namespace BuildSoft.Code.CsGenerator
{
    [Flags]
    public enum TypeDefinitionModifierFlags
    {
        // AccessModifiers
        Public = AccessModifier.Public << AccessModifierShiftBytes,
        Private = AccessModifier.Private << AccessModifierShiftBytes,
        Protected = AccessModifier.Protected << AccessModifierShiftBytes,
        Internal = AccessModifier.Internal << AccessModifierShiftBytes,
        ProtectedInternal = AccessModifier.ProtectedInternal << AccessModifierShiftBytes,
        PrivateProtected = AccessModifier.PrivateProtected << AccessModifierShiftBytes,

        // TypeAttributes
        New = TypeAttributeFlags.New << TypeAttributeFlagsShiftBytes,
        Abstract = TypeAttributeFlags.Abstract << TypeAttributeFlagsShiftBytes,
        Sealed = TypeAttributeFlags.Sealed << TypeAttributeFlagsShiftBytes,
        Static = TypeAttributeFlags.Static << TypeAttributeFlagsShiftBytes,
        Partial = TypeAttributeFlags.Partial << TypeAttributeFlagsShiftBytes,

        // Masks
        AccessModifierMask = AccessModifier.Mask << AccessModifierShiftBytes,
        TypeAttributeFlagsMask = TypeAttributeFlags.Mask << TypeAttributeFlagsShiftBytes,

        AccessModifierShiftBytes = 0,
        TypeAttributeFlagsShiftBytes = AccessModifier.UsingBytes + AccessModifierShiftBytes,
    }

    internal static class TypeDefinitionModifier
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static TypeDefinitionModifierFlags Union(AccessModifier accessModifier, TypeAttributeFlags typeAttribute)
        {
            return (TypeDefinitionModifierFlags)((EnumHelper.ToUInt64(typeAttribute) << (int)AccessModifier.UsingBytes) | EnumHelper.ToUInt64(accessModifier));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static void Deconstruct(this TypeDefinitionModifierFlags flags, out AccessModifier accessModifier, out TypeAttributeFlags typeAttribute)
        {
            TypeDefinitionModifierFlags maskedAttributeFlags = flags & TypeDefinitionModifierFlags.TypeAttributeFlagsMask;
            typeAttribute = (TypeAttributeFlags)(EnumHelper.ToUInt64(maskedAttributeFlags) >> (int)TypeDefinitionModifierFlags.TypeAttributeFlagsShiftBytes);
            accessModifier = (AccessModifier)(flags & TypeDefinitionModifierFlags.AccessModifierMask);
        }
    }
}

