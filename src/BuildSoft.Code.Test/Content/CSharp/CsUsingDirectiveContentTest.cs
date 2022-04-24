using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BuildSoft.Code.Content.CSharp.Test;

public class CsUsingDirectiveContentTest
{
    [Fact]
    public void ConstructorTest()
    {
        CsUsingDirectiveContent content = new("System");
        Assert.Equal("System", content.Namespace.Value);
        Assert.False(content.IsGlobal);

        content = new("System", true);
        Assert.Equal("System", content.Namespace.Value, true);
        Assert.True(content.IsGlobal);
    }

    [Fact]
    public void ToCodeNotGlobalTest()
    {
        CsUsingDirectiveContent content = new("System");

        string body = "using System;\r\n";
        Assert.Equal(new Code(body, body.Length, false, false), content.ToCode(""));

        body = " " + body;
        Assert.Equal(new Code(body, body.Length, false, false), content.ToCode(" "));
    }

    [Fact]
    public void ToCodeGlobalTest()
    {
        CsUsingDirectiveContent content = new("System", true);

        string body = "global using System;\r\n";
        Assert.Equal(new Code(body, body.Length, false, false), content.ToCode(""));
    }
}
