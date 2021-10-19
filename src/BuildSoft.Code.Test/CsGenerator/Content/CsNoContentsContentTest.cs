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
    public class CsNoContentsContentTest : CsNoContentsContent
    {
        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void AddContentTest()
        {
            AddContent(new CsLineContent());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void RemoveContentTest()
        {
            RemoveContent(new CsLineContent());
        }

        [TestMethod]
        [ExpectedException(typeof(InvalidOperationException))]
        public void ClearContentsTest()
        {
            ClearContents();
        }

        [TestMethod]
        public void ToCodeTest()
        {
            int i = 0;
            Assert.AreEqual(Code, ToCode(out int contentPosition, ref i));
            Assert.AreEqual(Code.Length, contentPosition);
            Assert.AreEqual(0, i);
        }

        const string Code = "code";
        public override string ToCode(int indent)
            => Code;
    }
}