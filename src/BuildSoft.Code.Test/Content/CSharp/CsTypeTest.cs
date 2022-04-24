using System;
using System.Collections.Generic;
using BuildSoft.Code.Test;
using Xunit;

namespace BuildSoft.Code.Content.CSharp.Test;

public class CsTypeTest
{
    [Fact]
    public void ConstructorTest1()
    {
        CsType type = new(typeof(int));
        Assert.Equal(new CsNamespace(typeof(int).Namespace), type.Namespace);
        Assert.Equal(new CsTypeName(typeof(int).Name), type.Name);
        Assert.Equal(typeof(int).Name, type.Value);
        Assert.Equal(typeof(int).FullName, type.FullName);
        Assert.False(type.IsGeneric);
    }

    [Fact]
    public void ConstructorTest2()
    {
        CsType type = new(new CsNamespace(typeof(int).Namespace), new CsTypeName(typeof(int).Name));
        Assert.Equal(new CsNamespace(typeof(int).Namespace), type.Namespace);
        Assert.Equal(new CsTypeName(typeof(int).Name), type.Name);
        Assert.Equal(typeof(int).Name, type.Value);
        Assert.Equal(typeof(int).FullName, type.FullName);
        Assert.False(type.IsGeneric);
    }

    [Fact]
    public void ConstructorTest3()
    {
        CsType type = new(new CsTypeName("Type"));
        Assert.Equal(CsNamespace.Global, type.Namespace);
        Assert.Equal(new CsTypeName("Type"), type.Name);
        Assert.Equal("Type", type.Value);
        Assert.Equal("Type", type.FullName);
        Assert.False(type.IsGeneric);
    }

    [Fact]
    public void ConstructorTest4()
    {
        CsType type = new("Type");
        Assert.Equal(CsNamespace.Global, type.Namespace);
        Assert.Equal(new CsTypeName("Type"), type.Name);
        Assert.Equal("Type", type.Value);
        Assert.Equal("Type", type.FullName);
        Assert.False(type.IsGeneric);

        type = new("global::Type");
        Assert.Equal(CsNamespace.Global, type.Namespace);
        Assert.Equal(new CsTypeName("Type"), type.Name);
        Assert.Equal("Type", type.Value);
        Assert.Equal("Type", type.FullName);
        Assert.False(type.IsGeneric);

        type = new("System.Type");
        Assert.Equal(new CsNamespace("System"), type.Namespace);
        Assert.Equal(new CsTypeName("Type"), type.Name);
        Assert.Equal("Type", type.Value);
        Assert.Equal("System.Type", type.FullName);
        Assert.False(type.IsGeneric);

        string expectedName = "Dictionary<System.String, System.Collections.Generic.List<System.String>>";
        type = new(typeof(Dictionary<string, List<string>>).FullName!);
        Assert.Equal(typeof(Dictionary<string, List<string>>).Namespace, type.Namespace.Value);
        Assert.Equal(new CsTypeName(expectedName), type.Name);
        Assert.Equal(expectedName, type.Value);
        Assert.Equal(typeof(Dictionary<string, List<string>>).Namespace + "." + expectedName, type.FullName);
        Assert.False(type.IsGeneric);

        Assert.Equal(new CsType(typeof(Dictionary<List<int[]>[], Dictionary<int[], string>>[]))
            , new CsType("System.Collections.Generic.Dictionary<System.Collections.Generic.List<System.Int32[]>[], System.Collections.Generic.Dictionary<System.Int32[], System.String>>[]"));
    }

    [Fact]
    public void GetOptimizedNameTest()
    {
        Assert.Equal("sbyte", new CsType(typeof(sbyte)).GetOptimizedName());
        Assert.Equal("byte", new CsType(typeof(byte)).GetOptimizedName());
        Assert.Equal("short", new CsType(typeof(short)).GetOptimizedName());
        Assert.Equal("ushort", new CsType(typeof(ushort)).GetOptimizedName());
        Assert.Equal("int", new CsType(typeof(int)).GetOptimizedName());
        Assert.Equal("uint", new CsType(typeof(uint)).GetOptimizedName());
        Assert.Equal("nuint", new CsType(typeof(nuint)).GetOptimizedName());
        Assert.Equal("nint", new CsType(typeof(nint)).GetOptimizedName());
        Assert.Equal("long", new CsType(typeof(long)).GetOptimizedName());
        Assert.Equal("ulong", new CsType(typeof(ulong)).GetOptimizedName());
        Assert.Equal("float", new CsType(typeof(float)).GetOptimizedName());
        Assert.Equal("double", new CsType(typeof(double)).GetOptimizedName());
        Assert.Equal("decimal", new CsType(typeof(decimal)).GetOptimizedName());
        Assert.Equal("char", new CsType(typeof(char)).GetOptimizedName());
        Assert.Equal("bool", new CsType(typeof(bool)).GetOptimizedName());
        Assert.Equal("object", new CsType(typeof(object)).GetOptimizedName());
        Assert.Equal("string", new CsType(typeof(string)).GetOptimizedName());
        Assert.Equal("void", new CsType(typeof(void)).GetOptimizedName());
        Assert.Equal("System.Math", new CsType(typeof(Math)).GetOptimizedName());
    }

    [WorkItem(3)]
    [Fact]
    public void GetOptimizedNameGenericsTest()
    {
        Assert.Equal("System.Collections.Generic.List<int>", new CsType(typeof(List<int>)).GetOptimizedName());
        Assert.Equal("System.Collections.Generic.List<System.Collections.Generic.Dictionary<int, string>>", new CsType(typeof(List<Dictionary<int, string>>)).GetOptimizedName());
        Assert.Equal("System.Collections.Generic.Dictionary<System.Collections.Generic.List<int>, System.Collections.Generic.Dictionary<int, string>>"
            , new CsType(typeof(Dictionary<List<int>, Dictionary<int, string>>)).GetOptimizedName());
    }

    [WorkItem(3)]
    [Fact]
    public void GetOptimizedNameArrayTest()
    {
        Assert.Equal("int[]", new CsType(typeof(int[])).GetOptimizedName());
        Assert.Equal("int[][]", new CsType(typeof(int[][])).GetOptimizedName());
        Assert.Equal("System.Collections.Generic.List<int>[]", new CsType(typeof(List<int>[])).GetOptimizedName());
        Assert.Equal("System.Collections.Generic.List<int>[]", new CsType(typeof(List<int>[])).GetOptimizedName());
        Assert.Equal("System.Collections.Generic.List<int[][]>[]", new CsType(typeof(List<int[][]>[])).GetOptimizedName());
        Assert.Equal("System.Collections.Generic.List<int[][,,]>[,]", new CsType(typeof(List<int[][,,]>[,])).GetOptimizedName());
    }

    [Fact]
    public void ToStringTest()
    {
        Assert.Equal(typeof(int).FullName, new CsType(typeof(int)).ToString());
        Assert.Equal(typeof(Math).FullName, new CsType(typeof(Math)).ToString());
    }

    [WorkItem(3)]
    [Fact]
    public void ToStringGenericsTest()
    {
        Assert.Equal("System.Collections.Generic.List<System.Int32>", new CsType(typeof(List<int>)).ToString());
        Assert.Equal("System.Collections.Generic.List<System.Collections.Generic.Dictionary<System.Int32, System.String>>", new CsType(typeof(List<Dictionary<int, string>>)).ToString());
        Assert.Equal("System.Collections.Generic.Dictionary<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.Dictionary<System.Int32, System.String>>"
            , new CsType(typeof(Dictionary<List<int>, Dictionary<int, string>>)).ToString());
        Assert.Equal(new CsType("System.Collections.Generic.Dictionary<System.Collections.Generic.List<System.Int32>, System.Collections.Generic.Dictionary<System.Int32, System.String>>")
            , new CsType(typeof(Dictionary<List<int>, Dictionary<int, string>>)));
    }

    [WorkItem(3)]
    [Fact]
    public void ToStringArrayTest()
    {
        Assert.Equal("System.Int32[]", new CsType(typeof(int[])).ToString());
        Assert.Equal("System.Int32[][]", new CsType(typeof(int[][])).ToString());
        Assert.Equal("System.Collections.Generic.List<System.Int32>[]", new CsType(typeof(List<int>[])).ToString());
        Assert.Equal("System.Collections.Generic.List<System.Int32[][]>[]", new CsType(typeof(List<int[][]>[])).ToString());
    }
}
