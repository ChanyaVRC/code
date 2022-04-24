using System;
using Xunit;

namespace BuildSoft.Code.Content.CSharp.Test;

public class CsContentTest : CsContent
{
    public override Code ToCode(string indent) => Code.CreateWithNoContents(indent);

    [Fact]
    public void AddableContentsTest()
    {
        CsLineContent line = new();

        Assert.Empty(Contents);

        AddableContents.Add(line);

        Assert.Single(Contents, line);
    }

    [Fact]
    public void CanOperateContentsTest()
    {
        Assert.True(CanOperateContents);

        AddContent(new CsLineContent());

        CanOperateContents = false;
        Assert.False(CanOperateContents);

        Assert.Throws<InvalidOperationException>(() => AddContent(new CsLineContent()));
    }

    [Fact]
    public void AddContentTest()
    {
        CsLineContent line = new();

        Assert.Empty(Contents);

        AddContent(line);

        Assert.Single(Contents, line);
    }

    [Fact]
    public void RemoveContentTest()
    {
        CsLineContent line1 = new();
        CsLineContent line2 = new();

        AddContent(line1);
        AddContent(line2);

        Assert.Equal(new[] { line1, line2 }, Contents);

        Assert.True(RemoveContent(line2));
        Assert.Single(Contents, line1);

        Assert.False(RemoveContent(line2));
        Assert.Single(Contents, line1);
    }

    [Fact]
    public void ToCodeTest()
    {
        int oldTabSize = CodeHelper.TabSize;

        CodeHelper.TabSize = 0;
        Assert.Equal("", ToCode(0).Body);
        Assert.Equal("", ToCode(1).Body);

        CodeHelper.TabSize = 2;
        Assert.Equal("", ToCode(0).Body);
        Assert.Equal("  ", ToCode(1).Body);

        CodeHelper.TabSize = oldTabSize;
    }
}
