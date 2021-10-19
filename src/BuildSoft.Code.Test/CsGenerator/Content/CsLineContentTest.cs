using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildSoft.Code.CsGenerator.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.CsGenerator.Content.Test
{
    [TestClass]
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