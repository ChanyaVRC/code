using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildSoft.Code.CsGenerator.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace BuildSoft.Code.CsGenerator.Content.Test
{
    [TestClass]
    [TestOf(typeof(CsFileContent))]
    public class CsFileContentTest
    {
        private readonly CsFileContent _exportToTestTarget = new();
        private string _exportedCode = string.Empty;
        private byte[] _expectedDefaultEncordingCode = null!;
        private byte[] _expectedUtf32EncordingCode = null!;

        [TestInitialize]
        public void InitializeForExportToTest()
        {
            _exportToTestTarget.AddContent(new CsUsingContent("System"));
            _exportToTestTarget.AddContent(new CsLineContent());

            CsNamespaceContent content = new("Test");
            content.AddContent(new CsNamespaceContent("Content"));
            _exportToTestTarget.AddContent(content);

            _exportedCode = _exportToTestTarget.Export();
            _expectedDefaultEncordingCode = Encoding.Default.GetBytes(_exportedCode);
            _expectedUtf32EncordingCode = Encoding.UTF32.GetBytes(_exportedCode);
        }

        [TestMethod]
        public void ConstructorTest()
        {
            _ = new CsFileContent();
        }

        [TestMethod]
        public void ToCodeTest()
        {
            CsFileContent content = new();

            int indent = 0;
            Assert.AreEqual("", content.ToCode(out int position, ref indent));
            Assert.AreEqual(0, position);
            Assert.AreEqual(0, indent);

            indent = 1;
            Assert.AreEqual("", content.ToCode(out position, ref indent));
            Assert.AreEqual(0, position);
            Assert.AreEqual(1, indent);
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

        [TestMethod]
        public void ExportAsStringTest()
        {
            CodeHelper.TabSize = 1;
            string expectedCode =
@"using System;

namespace Test
{
 namespace Content
 {

 }

}
";

            Assert.AreEqual(expectedCode, _exportToTestTarget.Export());
        }

        [TestMethod]
        public void ExportToStreamTest()
        {
            using var ms = new MemoryStream();
            _exportToTestTarget.Export(ms);
            CollectionAssert.AreEqual(_expectedDefaultEncordingCode, ms.ToArray());
        }

        [TestMethod]
        public void ExportToStreamEncordingTest()
        {
            using var ms = new MemoryStream();
            _exportToTestTarget.Export(ms, Encoding.UTF32);
            CollectionAssert.AreEqual(_expectedUtf32EncordingCode, ms.ToArray());
        }
    }
}