using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildSoft.Code.Content.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp.Test;

[TestClass]
[TestOf(typeof(CsNamespace))]
public class CsNamespaceTest
{

    [TestMethod]
    public void ConstructorTest1()
    {
        CsNamespace ns = new(new CsIdentifier("System"));
        Assert.AreEqual(1, ns.Domain.Length);
        Assert.AreEqual(new CsIdentifier("System"), ns.Domain[0]);
        Assert.AreEqual("System", ns.Value);
    }

    [TestMethod]
    public void ConstructorTest2()
    {
        string source = "System.Collections.Generic";
        CsNamespace ns = new(source);
        Assert.AreEqual(3, ns.Domain.Length);
        Assert.AreEqual(new CsIdentifier("System"), ns.Domain[0]);
        Assert.AreEqual(new CsIdentifier("Collections"), ns.Domain[1]);
        Assert.AreEqual(new CsIdentifier("Generic"), ns.Domain[2]);
        Assert.AreEqual(source, ns.Value);

        source = "";
        ns = new(source);
        Assert.AreEqual(0, ns.Domain.Length);
        Assert.IsNull(ns.Value);

        Assert.AreEqual(CsNamespace.Global, new CsNamespace(null));
    }

    [TestMethod]
    public void ConstructorTest3()
    {
        CsNamespace ns = new(new CsNamespace("System.Collections"), new CsIdentifier("Generic"));
        CsNamespace expected = new("System.Collections.Generic");
        Assert.AreEqual(expected, ns);

        ns = new(CsNamespace.Global, new CsIdentifier("BuildSoft"));
        expected = new("BuildSoft");
        Assert.AreEqual(expected, ns);
    }

    [TestMethod]
    public void ConstructorTest4()
    {
        CsNamespace ns = new(new CsNamespace("BuildSoft.Code"), "Content.CSharp");
        CsNamespace expected = new("BuildSoft.Code.Content.CSharp");
        Assert.AreEqual(expected, ns);

        ns = new(CsNamespace.Global, "Content.CSharp");
        expected = new("Content.CSharp");
        Assert.AreEqual(expected, ns);
    }

    [TestMethod]
    public void ConstructorTest5()
    {
        CsNamespace ns = new(new CsNamespace("BuildSoft.Code"), new CsNamespace("Content.CSharp"));
        CsNamespace expected = new("BuildSoft.Code.Content.CSharp");
        Assert.AreEqual(expected, ns);

        ns = new(CsNamespace.Global, new CsNamespace("Content.CSharp"));
        expected = new("Content.CSharp");
        Assert.AreEqual(expected, ns);

        ns = new(new CsNamespace("BuildSoft.Code"), CsNamespace.Global);
        expected = new("BuildSoft.Code");
        Assert.AreEqual(expected, ns);
    }

    [TestMethod]
    public void ToStringTest()
    {
        Assert.IsNull(CsNamespace.Global.ToString());
        Assert.AreEqual("BuildSoft.Code", new CsNamespace("BuildSoft.Code").ToString());
    }

    [TestMethod]
    public void GetHashCodeTest()
    {
        Assert.AreEqual(0, CsNamespace.Global.GetHashCode());
        Assert.AreEqual("BuildSoft.Code".GetHashCode(), new CsNamespace("BuildSoft.Code").GetHashCode());
    }

    [TestMethod]
    public void EqualsTest()
    {
        Assert.IsTrue(new CsNamespace("BuildSoft.Code").Equals((object?)new CsNamespace("BuildSoft.Code")));
        Assert.IsFalse(new CsNamespace("BuildSoft.Code").Equals((object?)new CsNamespace("BuildSoft.Card")));
        Assert.IsTrue(new CsNamespace("BuildSoft").Equals((object?)new CsNamespace((CsIdentifier)"BuildSoft")));
    }

    [TestMethod]
    public void EqualsTest1()
    {
        Assert.IsTrue(new CsNamespace("BuildSoft.Code").Equals(new CsNamespace("BuildSoft.Code")));
        Assert.IsFalse(new CsNamespace("BuildSoft.Code").Equals(new CsNamespace("BuildSoft.Card")));
        Assert.IsTrue(new CsNamespace("BuildSoft").Equals(new CsNamespace((CsIdentifier)"BuildSoft")));
    }
}
