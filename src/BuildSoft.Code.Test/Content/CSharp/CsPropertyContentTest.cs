using Xunit;

namespace BuildSoft.Code.Content.CSharp.Test;

public class CsPropertyContentTest
{
    [Fact()]
    public void ConstructorTest()
    {
        CsPropertyContent content = new("Property", "Type");
        Assert.NotNull(content);
    }

    [Fact()]
    public void ToCodeTest()
    {
        CsPropertyContent content = new("Property", "Type");

        string expectedBody =
@"Type Property
{
}
";
        Code expected = new(expectedBody, expectedBody.Length - "}\r\n".Length, true, true);
        Assert.Equal(expected, content.ToCode(""));

        expectedBody =
@" Type Property
 {
 }
";
        expected = new(expectedBody, expectedBody.Length - " }\r\n".Length, true, true);
        Assert.Equal(expected, content.ToCode(" "));
    }
}
