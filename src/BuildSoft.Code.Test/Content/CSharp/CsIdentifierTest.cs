using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildSoft.Code.Content.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp.Test;

[TestClass()]
[TestOf(typeof(CsIdentifier))]
public class CsIdentifierTest
{
    [TestMethod()]
    public void ConstructorTest()
    {
        const int Length = 10;
        for (int i = 0; i < Length; i++)
        {
            StringBuilder sb = new(Length);
            sb.Append('a', Length - 1);
            sb.Insert(i, '*');
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => new CsIdentifier(sb.ToString()));
            Assert.AreEqual("'*' cannot be used at an identifier. (Parameter 'value')", ex.Message);
            Assert.AreEqual("value", ex.ParamName);
        }
        {
            StringBuilder sb = new(Length);
            sb.Append('a', Length - 1);
            sb.Insert(0, '1');
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => new CsIdentifier(sb.ToString()));
            Assert.AreEqual("'1' cannot be used at the beginning of the identifier. (Parameter 'value')", ex.Message);
            Assert.AreEqual("value", ex.ParamName);
        }
        for (int i = 1; i < Length; i++)
        {
            StringBuilder sb = new(Length);
            sb.Append('a', Length - 1);
            sb.Insert(i, '1');
            CsIdentifier identifier = new(sb.ToString());
            Assert.AreEqual(sb.ToString(), identifier.Value);
        }
        {
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => new CsIdentifier("a*+b"));
            Assert.AreEqual("'*' cannot be used at an identifier. (Parameter 'value')", ex.Message);
            Assert.AreEqual("value", ex.ParamName);
        }
        {
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => new CsIdentifier("a＋absc"));
            Assert.AreEqual("'＋' cannot be used at an identifier. (Parameter 'value')", ex.Message);
            Assert.AreEqual("value", ex.ParamName);
        }
        {
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => new CsIdentifier("0*ab"));
            Assert.AreEqual("'0' cannot be used at the beginning of the identifier. (Parameter 'value')", ex.Message);
            Assert.AreEqual("value", ex.ParamName);
        }
        {
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => new CsIdentifier("０a"));
            Assert.AreEqual("'０' cannot be used at the beginning of the identifier. (Parameter 'value')", ex.Message);
            Assert.AreEqual("value", ex.ParamName);
        }
        {
            ArgumentException ex = Assert.ThrowsException<ArgumentException>(() => new CsIdentifier(""));
            Assert.AreEqual("Identifier must be at least one character. (Parameter 'value')", ex.Message);
            Assert.AreEqual("value", ex.ParamName);
        }
    }

    [TestMethod()]
    public void ToStringTest()
    {
        Assert.AreEqual("value", new CsIdentifier("value").ToString());
    }
}
