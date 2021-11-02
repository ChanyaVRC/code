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

            string body = "using System;\r\n";
            Assert.AreEqual(new Code(body, body.Length, false, false), content.ToCode(""));

            body = " " + body;
            Assert.AreEqual(new Code(body, body.Length, false, false), content.ToCode(" "));
        }
    }
}