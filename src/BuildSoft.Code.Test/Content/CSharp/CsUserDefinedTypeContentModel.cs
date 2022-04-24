using System.Collections.Generic;
using Xunit;

namespace BuildSoft.Code.Content.CSharp.Test;

public class CsUserDefinedTypeContentTest
{
    #region members for CsUserDefinedTypeContent
    private class CsUserDefinedTypeContentModel : CsUserDefinedTypeContent
    {

        public CsUserDefinedTypeContentModel() : base("identifier")
        {
        }

        public CsUserDefinedTypeContentModel(string identifier, IReadOnlyCollection<string>? modifiers = null, CsType? subClass = null, IReadOnlyCollection<CsType>? baseInterfaces = null)
            : base(identifier, modifiers, subClass, baseInterfaces)
        {
        }

        public override string Keyword => "keyword";
    }
    #endregion

    private static readonly string[] _modifiers = new[] { "public", "partial" };
    private static readonly CsType[] _interfaces = new CsType[] { "I1", "I2" };

    [Fact]
    public void ToCodeIndentTest()
    {
        CsUserDefinedTypeContentModel content = new("Identifier");
        string expectedBody =
@"  keyword Identifier
  {
  }
";
        Code expected = new(expectedBody, expectedBody.Length - "  }\r\n".Length, true, true);
        Assert.Equal(expected, content.ToCode("  "));
    }

    [Fact]
    public void ToCodeTest1()
    {
        CsUserDefinedTypeContentModel content = new("Identifier");
        string expectedBody =
@"keyword Identifier
{
}
";
        Code expected = new(expectedBody, expectedBody.Length - "}\r\n".Length, true, true);
        Assert.Equal(expected, content.ToCode(""));
    }

    [Fact]
    public void ToCodeTest2()
    {
        CsUserDefinedTypeContentModel content = new("Identifier", _modifiers);
        string expectedBody =
@"public partial keyword Identifier
{
}
";
        Code expected = new(expectedBody, expectedBody.Length - "}\r\n".Length, true, true);
        Assert.Equal(expected, content.ToCode(""));
    }

    [Fact]
    public void ToCodeTest3()
    {
        CsUserDefinedTypeContentModel content = new("Identifier", null, "Base");
        string expectedBody =
@"keyword Identifier : Base
{
}
";
        Code expected = new(expectedBody, expectedBody.Length - "}\r\n".Length, true, true);
        Assert.Equal(expected, content.ToCode(""));
    }

    [Fact]
    public void ToCodeTest4()
    {
        CsUserDefinedTypeContentModel content = new("Identifier", null, null, _interfaces);
        string expectedBody =
@"keyword Identifier : I1, I2
{
}
";
        Code expected = new(expectedBody, expectedBody.Length - "}\r\n".Length, true, true);
        Assert.Equal(expected, content.ToCode(""));
    }

    [Fact]
    public void ToCodeAllTest()
    {
        CsUserDefinedTypeContentModel content = new("Identifier", _modifiers, "Base", _interfaces);
        string expectedBody =
@"public partial keyword Identifier : Base, I1, I2
{
}
";
        Code expected = new(expectedBody, expectedBody.Length - "}\r\n".Length, true, true);
        Assert.Equal(expected, content.ToCode(""));
    }
}
