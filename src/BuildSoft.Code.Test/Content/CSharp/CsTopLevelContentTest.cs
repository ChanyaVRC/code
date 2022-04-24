using Xunit;

namespace BuildSoft.Code.Content.CSharp.Test;

public class CsTopLevelContentTest
{
    [Fact]
    public void ConstructorTest()
    {
        _ = new CsTopLevelContent();
    }

    [Fact]
    public void ToCodeTest()
    {
        CsTopLevelContent content = new();

        Assert.Equal(new Code("", 0, false, true), content.ToCode(""));
        Assert.Equal(new Code("", 0, false, true), content.ToCode(" "));
    }

    [Fact]
    public void AddNamespaceContentTest()
    {
        CsTopLevelContent content = new();
        CsNamespaceContent namespaceContent = new("Test");

        Assert.Empty(content.Contents);

        content.AddContent(namespaceContent);

        Assert.Single(content.Contents, namespaceContent);
    }

    [Fact]
    public void AddUsingContentTest()
    {
        CsTopLevelContent content = new();
        CsUsingDirectiveContent usingContent = new("System");

        Assert.Empty(content.Contents);

        content.AddContent(usingContent);

        Assert.Single(content.Contents, usingContent);
    }

    [Fact]
    public void RemoveNamespaceContentTest()
    {
        CsTopLevelContent testTarget = new();
        CsNamespaceContent namespaceContent1 = new("Test1");
        CsNamespaceContent namespaceContent2 = new("Test2");

        testTarget.AddContent(namespaceContent1);
        testTarget.AddContent(namespaceContent2);

        Assert.Equal(new[] { namespaceContent1, namespaceContent2 }, testTarget.Contents);

        Assert.True(testTarget.RemoveContent(namespaceContent2));
        Assert.Single(testTarget.Contents, namespaceContent1);

        Assert.False(testTarget.RemoveContent(namespaceContent2));
        Assert.Single(testTarget.Contents, namespaceContent1);
    }

    [Fact]
    public void RemoveUsingContentTest()
    {
        CsTopLevelContent testTarget = new();
        CsUsingDirectiveContent usingDirectiveContent1 = new("System");
        CsUsingDirectiveContent usingDirectiveContent2 = new("System.Collections.Generic");

        testTarget.AddContent(usingDirectiveContent1);
        testTarget.AddContent(usingDirectiveContent2);

        Assert.Equal(new[] { usingDirectiveContent1, usingDirectiveContent2 }, testTarget.Contents);

        Assert.True(testTarget.RemoveContent(usingDirectiveContent2));
        Assert.Single(testTarget.Contents, usingDirectiveContent1);

        Assert.False(testTarget.RemoveContent(usingDirectiveContent2));
        Assert.Single(testTarget.Contents, usingDirectiveContent1);
    }
}


