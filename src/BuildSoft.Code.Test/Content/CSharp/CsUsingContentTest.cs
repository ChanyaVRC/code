using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp.Test
{
    [TestClass]
    [TestOf(typeof(CsUsingContent))]
    public class CsUsingContentTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            CsUsingContent content = new("System");
            Assert.AreEqual("System", content.Namespace);
        }

        [TestMethod]
        public void ToCodeTest()
        {
            CsUsingContent content = new("System");

            CodeHelper.TabSize = 1;
            Assert.AreEqual("using System;\r\n", content.ToCode(0));
            Assert.AreEqual(" using System;\r\n", content.ToCode(1));
            CodeHelper.TabSize = 2;
            Assert.AreEqual("  using System;\r\n", content.ToCode(1));
        }
    }
}