using Xunit;

namespace BuildSoft.Code.Content.CSharp.Test;

public class CsLineContentTest
{
    [Fact]
    public void ConstructorTest()
    {
        CsLineContent line = new();
        Assert.False(line.CanOperateContents);
    }

    [Fact]
    public void ToCodeTest()
    {
        CsLineContent line = new();
        Assert.Equal(new Code("\r\n", 2, false, false), line.ToCode(""));
        Assert.Equal(new Code("\r\n", 2, false, false), line.ToCode(" "));
    }
}
