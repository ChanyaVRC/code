using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp;

internal static class CsContentResolver
{
    public static bool IsKeywordType(this string value) => KeywordToFullName.ContainsKey(value);
    public static bool IsFullNameOfKeywordType(this string fullName) => FullNameToKeyword.ContainsKey(fullName);
    public static string ToKeywordType(this string fullName) => FullNameToKeyword[fullName];
    public static string ToFullname(this string value) => KeywordToFullName[value];

    internal static Dictionary<string, string> FullNameToKeyword = new();
    internal static Dictionary<string, string> KeywordToFullName = new()
    {
        { "sbyte", "System.SByte" },
        { "byte", "System.Byte" },
        { "short", "System.Int16" },
        { "ushort", "System.UInt16" },
        { "int", "System.Int32" },
        { "uint", "System.UInt32" },
        { "long", "System.Int64" },
        { "ulong", "System.UInt64" },
        { "decimal", "System.Decimal" },
        { "float", "System.Single" },
        { "double", "System.Double" },
        { "char", "System.Char" },
        { "bool", "System.Boolean" },
        { "nint", "System.IntPtr" },
        { "nuint", "System.UIntPtr" },
        { "string", "System.String" },
        { "object", "System.Object" },
        { "void", "System.Void" },
    };

    static CsContentResolver()
    {
        foreach (var (k, v) in KeywordToFullName)
        {
            FullNameToKeyword.Add(v, k);
        }
    }
}
