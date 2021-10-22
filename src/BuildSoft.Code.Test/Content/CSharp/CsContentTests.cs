using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp.Test
{
    [TestClass]
    [TestOf(typeof(CsContent))]
    public class CsContentTests
    {
        private class CsContentDerived : CsContent
        {
            public override string ToCode(out int contentPosition, ref int contentIndent) 
                => throw new InvalidOperationException();
        }

        [TestMethod]
        public void AddContentTest()
        {
            CsContentDerived content = new();
            CsLineContent line = new();

            Assert.AreEqual(0, content.Contents.Count);
            
            content.AddContent(line);
         
            Assert.AreEqual(1, content.Contents.Count);
            Assert.AreSame(line, content.Contents[0]);
        }

        [TestMethod]
        public void RemoveContentTest()
        {
            CsContentDerived content = new();
            CsLineContent line1 = new();
            CsLineContent line2 = new();

            content.AddContent(line1);
            content.AddContent(line2);

            Assert.AreEqual(2, content.Contents.Count);

            Assert.IsTrue(content.RemoveContent(line2));
            Assert.AreEqual(1, content.Contents.Count);
            Assert.AreSame(line1, content.Contents[0]);

            Assert.IsFalse(content.RemoveContent(line2));
            Assert.AreEqual(1, content.Contents.Count);
            Assert.AreSame(line1, content.Contents[0]);
        }

        [TestMethod]
        public void ClearContentsTest()
        {
            CsContentDerived content = new();
            CsLineContent line1 = new();
            CsLineContent line2 = new();

            content.AddContent(line1);
            content.AddContent(line2);

            Assert.AreEqual(2, content.Contents.Count);
            content.ClearContents();
            Assert.AreEqual(0, content.Contents.Count);
        }
    }
}