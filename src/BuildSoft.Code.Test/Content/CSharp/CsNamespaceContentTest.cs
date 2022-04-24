using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace BuildSoft.Code.Content.CSharp.Test;

public class CsNamespaceContentTest
{
    [Fact]
    public void ConstructorTest()
    {
        CsNamespaceContent content = new("Test");
        Assert.Equal("Test", content.Namespace.Value);
        Assert.Empty(content.Contents);
    }

    [Fact]
    public void AddNamespaceContentTest()
    {
        CsNamespaceContent content = new("Parent");
        CsNamespaceContent childContent = new("Child");

        content.AddContent(childContent);
        Assert.Single(content.Contents, childContent);
    }

    [Fact]
    public void RemoveNamespaceContentTest()
    {
        CsNamespaceContent content = new("Parent");
        CsNamespaceContent childContent1 = new("Child1");
        CsNamespaceContent childContent2 = new("Child2");

        content.AddContent(childContent1);
        content.AddContent(childContent2);

        Assert.Equal(new[] { childContent1, childContent2 }, content.Contents);

        Assert.True(content.RemoveContent(childContent2));
        Assert.Single(content.Contents, childContent1);

        Assert.False(content.RemoveContent(childContent2));
        Assert.Single(content.Contents, childContent1);
    }

    [Fact]
    public void ToCodeTest()
    {
        CsNamespaceContent content = new("Test");

        string expectedBody =
@"namespace Test
{
}
";
        int expectedPosition =
@"namespace Test
{
".Length;
        Code expected = new(expectedBody, expectedPosition, true, true);
        Assert.Equal(expected, content.ToCode(""));

        expectedBody =
@" namespace Test
 {
 }
";
        expectedPosition =
@" namespace Test
 {
".Length;
        expected = new(expectedBody, expectedPosition, true, true);
        Assert.Equal(expected, content.ToCode(" "));
    }
}
