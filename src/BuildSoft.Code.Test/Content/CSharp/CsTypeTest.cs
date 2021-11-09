using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildSoft.Code.Content.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp.Test
{
    [TestClass()]
    [TestOf(typeof(CsType))]
    public class CsTypeTest
    {
        [TestMethod()]
        public void ConstructorTest1()
        {
            CsType type = new(typeof(int));
            Assert.AreEqual(new CsNamespace(typeof(int).Namespace), type.Namespace);
            Assert.AreEqual(new CsIdentifier(typeof(int).Name), type.Name);
            Assert.AreEqual(typeof(int).Name, type.Value);
            Assert.AreEqual(typeof(int).FullName, type.FullName);
            Assert.IsFalse(type.IsGeneric);
        }

        [TestMethod()]
        public void ConstructorTest2()
        {
            CsType type = new(new CsNamespace(typeof(int).Namespace), new CsIdentifier(typeof(int).Name));
            Assert.AreEqual(new CsNamespace(typeof(int).Namespace), type.Namespace);
            Assert.AreEqual(new CsIdentifier(typeof(int).Name), type.Name);
            Assert.AreEqual(typeof(int).Name, type.Value);
            Assert.AreEqual(typeof(int).FullName, type.FullName);
            Assert.IsFalse(type.IsGeneric);
        }

        [TestMethod()]
        public void ConstructorTest3()
        {
            CsType type = new(new CsIdentifier("Type"));
            Assert.AreEqual(CsNamespace.Global, type.Namespace);
            Assert.AreEqual(new CsIdentifier("Type"), type.Name);
            Assert.AreEqual("Type", type.Value);
            Assert.AreEqual("Type", type.FullName);
            Assert.IsFalse(type.IsGeneric);
        }

        [TestMethod()]
        public void ConstructorTest4()
        {
            CsType type = new("Type");
            Assert.AreEqual(CsNamespace.Global, type.Namespace);
            Assert.AreEqual(new CsIdentifier("Type"), type.Name);
            Assert.AreEqual("Type", type.Value);
            Assert.AreEqual("Type", type.FullName);
            Assert.IsFalse(type.IsGeneric);

            type = new("global::Type");
            Assert.AreEqual(CsNamespace.Global, type.Namespace);
            Assert.AreEqual(new CsIdentifier("Type"), type.Name);
            Assert.AreEqual("Type", type.Value);
            Assert.AreEqual("Type", type.FullName);
            Assert.IsFalse(type.IsGeneric);

            type = new("System.Type");
            Assert.AreEqual(new CsNamespace("System"), type.Namespace);
            Assert.AreEqual(new CsIdentifier("Type"), type.Name);
            Assert.AreEqual("Type", type.Value);
            Assert.AreEqual("System.Type", type.FullName);
            Assert.IsFalse(type.IsGeneric);
        }

        [TestMethod()]
        public void GetOptimizedNameTest()
        {
            Assert.AreEqual("sbyte", new CsType(typeof(sbyte)).GetOptimizedName());
            Assert.AreEqual("byte", new CsType(typeof(byte)).GetOptimizedName());
            Assert.AreEqual("short", new CsType(typeof(short)).GetOptimizedName());
            Assert.AreEqual("ushort", new CsType(typeof(ushort)).GetOptimizedName());
            Assert.AreEqual("int", new CsType(typeof(int)).GetOptimizedName());
            Assert.AreEqual("uint", new CsType(typeof(uint)).GetOptimizedName());
            Assert.AreEqual("nuint", new CsType(typeof(nuint)).GetOptimizedName());
            Assert.AreEqual("nint", new CsType(typeof(nint)).GetOptimizedName());
            Assert.AreEqual("long", new CsType(typeof(long)).GetOptimizedName());
            Assert.AreEqual("ulong", new CsType(typeof(ulong)).GetOptimizedName());
            Assert.AreEqual("float", new CsType(typeof(float)).GetOptimizedName());
            Assert.AreEqual("double", new CsType(typeof(double)).GetOptimizedName());
            Assert.AreEqual("decimal", new CsType(typeof(decimal)).GetOptimizedName());
            Assert.AreEqual("char", new CsType(typeof(char)).GetOptimizedName());
            Assert.AreEqual("bool", new CsType(typeof(bool)).GetOptimizedName());
            Assert.AreEqual("object", new CsType(typeof(object)).GetOptimizedName());
            Assert.AreEqual("string", new CsType(typeof(string)).GetOptimizedName());
            Assert.AreEqual("void", new CsType(typeof(void)).GetOptimizedName());
            Assert.AreEqual(typeof(Math).FullName, new CsType(typeof(Math)).GetOptimizedName());
            Assert.AreEqual(typeof(List<int>).FullName, new CsType(typeof(List<int>)).ToString());
        }

        [TestMethod()]
        public void ToStringTest()
        {
            Assert.AreEqual(typeof(int).FullName, new CsType(typeof(int)).ToString());
            Assert.AreEqual(typeof(Math).FullName, new CsType(typeof(Math)).ToString());
            Assert.AreEqual(typeof(List<int>).FullName, new CsType(typeof(List<int>)).ToString());
        }
    }
}
