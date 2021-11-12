using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp.Test;

[TestClass]
[TestOf(typeof(CsUsingDirectiveContent))]
public class CsUsingDirectiveContentTest
{
    [TestMethod]
    public void ConstructorTest()
    {
        CsUsingDirectiveContent content = new("System");
        Assert.AreEqual("System", content.Namespace.Value);
        Assert.IsFalse(content.IsGlobal);

        content = new("System", true);
        Assert.AreEqual("System", content.Namespace.Value, true);
        Assert.IsTrue(content.IsGlobal);
    }

    [TestMethod]
    public void ToCodeNotGlobalTest()
    {
        CsUsingDirectiveContent content = new("System");

        string body = "using System;\r\n";
        Assert.AreEqual(new Code(body, body.Length, false, false), content.ToCode(""));

        body = " " + body;
        Assert.AreEqual(new Code(body, body.Length, false, false), content.ToCode(" "));
    }

    [TestMethod]
    public void ToCodeGlobalTest()
    {
        CsUsingDirectiveContent content = new("System", true);

        string body = "global using System;\r\n";
        Assert.AreEqual(new Code(body, body.Length, false, false), content.ToCode(""));
    }
}
