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
    public class CsUserDefinedTypeContentTest :CsUserDefinedTypeContent
    {
        public CsUserDefinedTypeContentTest() : base("identifier")
        {
        }

        public CsUserDefinedTypeContentTest(string identifier, IReadOnlyCollection<string>? modifiers = null, string? subClass = null, IReadOnlyCollection<string>? baseInterfaces = null) 
            : base(identifier, modifiers, subClass, baseInterfaces)
        {
        }

        public override string Keyword => "keyword";

        [TestMethod()]
        public void ToCodeTest()
        {
            //Assert.Fail();
        }
    }
}