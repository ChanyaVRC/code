using Xunit;

namespace BuildSoft.Code.Content.CSharp.Test;

public class CsMethodContentTest
{
    private static readonly CsArgumentDefinition[] _arguments =
        new CsArgumentDefinition[] { new("int", "arg1"), new("string", "arg2", "in") };
    private static readonly string[] _modifier =
        new string[] { "public", "async" };

    [Fact]
    public void ConstructorTest()
    {
        CsMethodContent content = new("Identifier", "ReturnType");
        Assert.Empty(content.Arguments);
        Assert.Equal("", content.ArgumentList);

        content = new("Identifier", "ReturnType", null, _arguments);
        Assert.NotSame(_arguments, content.Arguments);
        Assert.Equal(_arguments, content.Arguments);
        Assert.Equal("int arg1, in string arg2", content.ArgumentList);
    }

    [Fact]
    public void ToCodeTest()
    {
        CsMethodContent content = new("Identifier", "ReturnType", _modifier, _arguments);

        string expectedBody =
@"public async ReturnType Identifier(int arg1, in string arg2)
{
}
";
        Code expected = new(expectedBody, expectedBody.Length - "}\r\n".Length, true, true);
        Assert.Equal(expected, content.ToCode(""));

        expectedBody =
@"  public async ReturnType Identifier(int arg1, in string arg2)
  {
  }
";
        expected = new(expectedBody, expectedBody.Length - "  }\r\n".Length, true, true);
        Assert.Equal(expected, content.ToCode("  "));
    }
}
