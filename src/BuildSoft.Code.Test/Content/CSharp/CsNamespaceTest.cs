using Xunit;

namespace BuildSoft.Code.Content.CSharp.Test;

public class CsNamespaceTest
{

    [Fact]
    public void ConstructorTest1()
    {
        CsNamespace ns = new(new CsIdentifier("System"));
        Assert.Equal(new[] { new CsIdentifier("System") }, ns.Domain);
        Assert.Equal("System", ns.Value);
    }

    [Fact]
    public void ConstructorTest2()
    {
        string source = "System.Collections.Generic";
        CsNamespace ns = new(source);
        Assert.Equal(3, ns.Domain.Length);
        Assert.Equal(new CsIdentifier("System"), ns.Domain[0]);
        Assert.Equal(new CsIdentifier("Collections"), ns.Domain[1]);
        Assert.Equal(new CsIdentifier("Generic"), ns.Domain[2]);
        Assert.Equal(source, ns.Value);

        source = "";
        ns = new(source);
        Assert.Empty(ns.Domain);
        Assert.Null(ns.Value);

        Assert.Equal(CsNamespace.Global, new CsNamespace(null));
    }

    [Fact]
    public void ConstructorTest3()
    {
        CsNamespace ns = new(new CsNamespace("System.Collections"), new CsIdentifier("Generic"));
        CsNamespace expected = new("System.Collections.Generic");
        Assert.Equal(expected, ns);

        ns = new(CsNamespace.Global, new CsIdentifier("BuildSoft"));
        expected = new("BuildSoft");
        Assert.Equal(expected, ns);
    }

    [Fact]
    public void ConstructorTest4()
    {
        CsNamespace ns = new(new CsNamespace("BuildSoft.Code"), "Content.CSharp");
        CsNamespace expected = new("BuildSoft.Code.Content.CSharp");
        Assert.Equal(expected, ns);

        ns = new(CsNamespace.Global, "Content.CSharp");
        expected = new("Content.CSharp");
        Assert.Equal(expected, ns);
    }

    [Fact]
    public void ConstructorTest5()
    {
        CsNamespace ns = new(new CsNamespace("BuildSoft.Code"), new CsNamespace("Content.CSharp"));
        CsNamespace expected = new("BuildSoft.Code.Content.CSharp");
        Assert.Equal(expected, ns);

        ns = new(CsNamespace.Global, new CsNamespace("Content.CSharp"));
        expected = new("Content.CSharp");
        Assert.Equal(expected, ns);

        ns = new(new CsNamespace("BuildSoft.Code"), CsNamespace.Global);
        expected = new("BuildSoft.Code");
        Assert.Equal(expected, ns);
    }

    [Fact]
    public void ToStringTest()
    {
        Assert.Null(CsNamespace.Global.ToString());
        Assert.Equal("BuildSoft.Code", new CsNamespace("BuildSoft.Code").ToString());
    }

    [Fact]
    public void GetHashCodeTest()
    {
        Assert.Equal(0, CsNamespace.Global.GetHashCode());
        Assert.Equal("BuildSoft.Code".GetHashCode(), new CsNamespace("BuildSoft.Code").GetHashCode());
    }

    [Fact]
    public void EqualsTest()
    {
        Assert.True(new CsNamespace("BuildSoft.Code").Equals((object?)new CsNamespace("BuildSoft.Code")));
        Assert.False(new CsNamespace("BuildSoft.Code").Equals((object?)new CsNamespace("BuildSoft.Card")));
        Assert.True(new CsNamespace("BuildSoft").Equals((object?)new CsNamespace((CsIdentifier)"BuildSoft")));
    }

    [Fact]
    public void EqualsTest1()
    {
        Assert.True(new CsNamespace("BuildSoft.Code").Equals(new CsNamespace("BuildSoft.Code")));
        Assert.False(new CsNamespace("BuildSoft.Code").Equals(new CsNamespace("BuildSoft.Card")));
        Assert.True(new CsNamespace("BuildSoft").Equals(new CsNamespace((CsIdentifier)"BuildSoft")));
    }
}
