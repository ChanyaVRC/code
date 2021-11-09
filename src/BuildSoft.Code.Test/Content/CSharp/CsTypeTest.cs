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
            CsType type = new(typeof(int));
            Assert.AreEqual("int", type.GetOptimizedName());
        }

        [TestMethod()]
        public void ToStringTest()
        {
            CsType type = new(typeof(int));
            Assert.AreEqual(type.FullName, type.ToString());
        }
    }
}
