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
    [TestOf(typeof(CsFieldContent))]
    public class CsFieldContentTest
    {
        [TestMethod()]
        public void ConstructorTest()
        {
            string[] modifiers = new string[] { "public", "volatile" };

            CsFieldContent content = new("identifier", "int", modifiers);
            
            Assert.AreEqual("identifier", content.Identifier);
            Assert.AreEqual("int", content.Type);
            CollectionAssert.AreEqual(modifiers, content.Modifiers.ToArray());
        }

        [TestMethod()]
        public void ToCodeTest()
        {
            CsFieldContent content = new("identifier", "int", new string[] { "public", "volatile" });

            string expectedBody = "public volatile int identifier;\r\n";
            Assert.AreEqual(new Code(expectedBody, expectedBody.Length, false, false), content.ToCode(""));

            expectedBody = " public volatile int identifier;\r\n";
            Assert.AreEqual(new Code(expectedBody, expectedBody.Length, false, false), content.ToCode(" "));
        }
    }
}