using System.Linq;
using Xunit;

namespace BuildSoft.Code.Content.CSharp.Test;

public class CsFieldContentTest
{
    [Fact()]
    public void ConstructorTest()
    {
        string[] modifiers = new string[] { "public", "volatile" };

        CsFieldContent content = new("identifier", "int", modifiers);

        Assert.Equal("identifier", content.Identifier.Value);
        Assert.Equal("Int32", content.Type.Value);
        Assert.Equal("int", content.Type.GetOptimizedName());
        Assert.Equal(modifiers, content.Modifiers.ToArray());
    }

    [Fact()]
    public void ToCodeTest()
    {
        CsFieldContent content = new("identifier", "int", new string[] { "public", "volatile" });

        string expectedBody = "public volatile int identifier;\r\n";
        Assert.Equal(new Code(expectedBody, expectedBody.Length, false, false), content.ToCode(""));

        expectedBody = " public volatile int identifier;\r\n";
        Assert.Equal(new Code(expectedBody, expectedBody.Length, false, false), content.ToCode(" "));
    }
}
