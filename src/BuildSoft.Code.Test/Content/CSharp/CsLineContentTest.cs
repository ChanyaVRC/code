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
        public void ToCodeTest()
        {
            CsLineContent line = new();
            Assert.AreEqual("\r\n", line.ToCode(indent: 0));
            Assert.AreEqual("\r\n", line.ToCode(indent: 1));
        }
    }
}