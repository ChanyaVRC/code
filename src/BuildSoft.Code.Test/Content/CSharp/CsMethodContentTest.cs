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
    [TestOf(typeof(CsMethodContent))]
    public class CsMethodContentTest
    {
        private static readonly CsArgumentDefinition[] _arguments =
            new CsArgumentDefinition[] { new("int", "arg1"), new("string", "arg2") };
        private static readonly string[] _modifier =
            new string[] { "public", "async" };
        
        [TestMethod()]
        public void ConstructorTest()
        {
            CsMethodContent content = new("Identifier", "ReturnType");
            Assert.AreEqual(0, content.Arguments.Count);
            Assert.AreEqual("", content.ArgumentList);

            content = new("Identifier", "ReturnType", null, _arguments);
            Assert.AreNotSame(_arguments, content.Arguments);
            Assert.AreEqual(2, content.Arguments.Count);
            Assert.AreEqual("int arg1, string arg2", content.ArgumentList);
        }

        [TestMethod()]
        public void ToCodeTest()
        {
            CsMethodContent content = new("Identifier", "ReturnType", _modifier, _arguments);

            string expectedBody =
@"public async ReturnType Identifier(int arg1, string arg2)
{
}
";
            Code expected = new(expectedBody, expectedBody.Length - "}\r\n".Length, true, true);
            Assert.AreEqual(expected, content.ToCode(""));

            expectedBody =
@"  public async ReturnType Identifier(int arg1, string arg2)
  {
  }
";
            expected = new(expectedBody, expectedBody.Length - "  }\r\n".Length, true, true);
            Assert.AreEqual(expected, content.ToCode("  "));
        }
    }
}