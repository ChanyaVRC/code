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
    [TestOf(typeof(CsUserDefinedTypeContent))]
    public class CsUserDefinedTypeContentTest : CsUserDefinedTypeContent
    {
        #region members for CsUserDefinedTypeContent
        public CsUserDefinedTypeContentTest() : base("identifier")
        {
        }

        public CsUserDefinedTypeContentTest(string identifier, IReadOnlyCollection<string>? modifiers = null, CsType? subClass = null, IReadOnlyCollection<string>? baseInterfaces = null)
            : base(identifier, modifiers, subClass, baseInterfaces)
        {
        }

        public override string Keyword => "keyword";
        #endregion

        private static readonly string[] _modifiers = new[] { "public", "partial" };
        private static readonly string[] _interfaces = new[] { "I1", "I2" };

        [TestMethod()]
        public void ToCodeIndentTest()
        {
            CsUserDefinedTypeContentTest content = new("Identifier");
            string expectedBody =
@"  keyword Identifier
  {
  }
";
            Code expected = new(expectedBody, expectedBody.Length - "  }\r\n".Length, true, true);
            Assert.AreEqual(expected, content.ToCode("  "));
        }

        [TestMethod()]
        public void ToCodeTest1()
        {
            CsUserDefinedTypeContentTest content = new("Identifier");
            string expectedBody =
@"keyword Identifier
{
}
";
            Code expected = new(expectedBody, expectedBody.Length - "}\r\n".Length, true, true);
            Assert.AreEqual(expected, content.ToCode(""));
        }

        [TestMethod()]
        public void ToCodeTest2()
        {
            CsUserDefinedTypeContentTest content = new("Identifier", _modifiers);
            string expectedBody =
@"public partial keyword Identifier
{
}
";
            Code expected = new(expectedBody, expectedBody.Length - "}\r\n".Length, true, true);
            Assert.AreEqual(expected, content.ToCode(""));
        }

        [TestMethod()]
        public void ToCodeTest3()
        {
            CsUserDefinedTypeContentTest content = new("Identifier", null, "Base");
            string expectedBody =
@"keyword Identifier : Base
{
}
";
            Code expected = new(expectedBody, expectedBody.Length - "}\r\n".Length, true, true);
            Assert.AreEqual(expected, content.ToCode(""));
        }

        [TestMethod()]
        public void ToCodeTest4()
        {
            CsUserDefinedTypeContentTest content = new("Identifier", null, null, _interfaces);
            string expectedBody =
@"keyword Identifier : I1, I2
{
}
";
            Code expected = new(expectedBody, expectedBody.Length - "}\r\n".Length, true, true);
            Assert.AreEqual(expected, content.ToCode(""));
        }

        [TestMethod()]
        public void ToCodeAllTest()
        {
            CsUserDefinedTypeContentTest content = new("Identifier", _modifiers, "Base", _interfaces);
            string expectedBody =
@"public partial keyword Identifier : Base, I1, I2
{
}
";
            Code expected = new(expectedBody, expectedBody.Length - "}\r\n".Length, true, true);
            Assert.AreEqual(expected, content.ToCode(""));
        }
    }
}