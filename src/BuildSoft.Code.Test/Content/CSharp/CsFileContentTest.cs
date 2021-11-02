using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp.Test
{
    [TestClass]
    [TestOf(typeof(CsFileContent))]
    public class CsFileContentTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            _ = new CsFileContent();
        }

        [TestMethod]
        public void ToCodeTest()
        {
            CsFileContent content = new();

            Assert.AreEqual(new Code("", 0, false, true), content.ToCode(""));
            Assert.AreEqual(new Code("", 0, false, true), content.ToCode(" "));
        }

        [TestMethod]
        public void AddNamespaceContentTest()
        {
            CsFileContent content = new();
            CsNamespaceContent namespaceContent = new("Test");

            Assert.AreEqual(0, content.Contents.Count);

            content.AddContent(namespaceContent);

            Assert.AreEqual(1, content.Contents.Count);
            Assert.AreSame(namespaceContent, content.Contents[0]);
        }

        [TestMethod]
        public void AddUsingContentTest()
        {
            CsFileContent content = new();
            CsUsingContent usingContent = new("System");

            Assert.AreEqual(0, content.Contents.Count);

            content.AddContent(usingContent);

            Assert.AreEqual(1, content.Contents.Count);
            Assert.AreSame(usingContent, content.Contents[0]);
        }

        [TestMethod]
        public void RemoveNamespaceContentTest()
        {
            CsFileContent testTarget = new();
            CsNamespaceContent namespaceContent1 = new("Test1");
            CsNamespaceContent namespaceContent2 = new("Test2");

            testTarget.AddContent(namespaceContent1);
            testTarget.AddContent(namespaceContent2);

            Assert.AreEqual(2, testTarget.Contents.Count);

            Assert.IsTrue(testTarget.RemoveContent(namespaceContent2));
            Assert.AreEqual(1, testTarget.Contents.Count);
            Assert.AreSame(namespaceContent1, testTarget.Contents[0]);

            Assert.IsFalse(testTarget.RemoveContent(namespaceContent2));
            Assert.AreEqual(1, testTarget.Contents.Count);
            Assert.AreSame(namespaceContent1, testTarget.Contents[0]);
        }

        [TestMethod]
        public void RemoveUsingContentTest()
        {
            CsFileContent testTarget = new();
            CsUsingContent namespaceContent1 = new("System");
            CsUsingContent namespaceContent2 = new("System.Collections.Generic");

            testTarget.AddContent(namespaceContent1);
            testTarget.AddContent(namespaceContent2);

            Assert.AreEqual(2, testTarget.Contents.Count);

            Assert.IsTrue(testTarget.RemoveContent(namespaceContent2));
            Assert.AreEqual(1, testTarget.Contents.Count);
            Assert.AreSame(namespaceContent1, testTarget.Contents[0]);

            Assert.IsFalse(testTarget.RemoveContent(namespaceContent2));
            Assert.AreEqual(1, testTarget.Contents.Count);
            Assert.AreSame(namespaceContent1, testTarget.Contents[0]);
        }
    }
}


