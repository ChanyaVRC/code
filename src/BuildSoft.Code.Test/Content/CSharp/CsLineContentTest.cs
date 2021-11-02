using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp.Test
{
    [TestClass]
    [TestOf(typeof(CsLineContent))]
    public class CsLineContentTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            CsLineContent line = new();
            Assert.IsFalse(line.CanOperateContents);
        }

        [TestMethod]
        public void ToCodeTest()
        {
            CsLineContent line = new();
            Assert.AreEqual(new Code("\r\n", 2, false, false), line.ToCode(""));
            Assert.AreEqual(new Code("\r\n", 2, false, false), line.ToCode(" "));
        }
    }
}