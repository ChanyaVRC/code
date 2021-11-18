using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BuildSoft.Code.Content.CSharp.Test;

[TestClass]
[TestOf(typeof(CsContent))]
public class CsContentTest : CsContent
{
    public override Code ToCode(string indent) => Code.CreateWithNoContents(indent);

    [TestMethod]
    public void AddableContentsTest()
    {
        CsLineContent line = new();

        Assert.AreEqual(0, Contents.Count);

        AddableContents.Add(line);

        Assert.AreEqual(1, Contents.Count);
        Assert.AreSame(line, Contents[0]);
    }

    [TestMethod]
    public void CanOperateContentsTest()
    {
        Assert.IsTrue(CanOperateContents);

        AddContent(new CsLineContent());

        CanOperateContents = false;
        Assert.IsFalse(CanOperateContents);

        Assert.ThrowsException<InvalidOperationException>(() => AddContent(new CsLineContent()));
    }

    [TestMethod]
    public void AddContentTest()
    {
        CsLineContent line = new();

        Assert.AreEqual(0, Contents.Count);

        AddContent(line);

        Assert.AreEqual(1, Contents.Count);
        Assert.AreSame(line, Contents[0]);
    }

    [TestMethod]
    public void RemoveContentTest()
    {
        CsLineContent line1 = new();
        CsLineContent line2 = new();

        AddContent(line1);
        AddContent(line2);

        Assert.AreEqual(2, Contents.Count);

        Assert.IsTrue(RemoveContent(line2));
        Assert.AreEqual(1, Contents.Count);
        Assert.AreSame(line1, Contents[0]);

        Assert.IsFalse(RemoveContent(line2));
        Assert.AreEqual(1, Contents.Count);
        Assert.AreSame(line1, Contents[0]);
    }

    [TestMethod]
    public void ToCodeTest()
    {
        CodeHelper.TabSize = 0;
        Assert.AreEqual("", ToCode(0).Body);
        Assert.AreEqual("", ToCode(1).Body);

        CodeHelper.TabSize = 2;
        Assert.AreEqual("", ToCode(0).Body);
        Assert.AreEqual("  ", ToCode(1).Body);
    }
}
