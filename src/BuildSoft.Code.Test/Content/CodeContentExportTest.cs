using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Text;
using System.IO;
using BuildSoft.Code.Content.CSharp;

namespace BuildSoft.Code.Content.Test
{
    [TestClass]
    [TestOf(typeof(CodeContent<>))]
    public class CodeContentExportTest
    {
        private readonly CsTopLevelContent _exportToTestTarget = new();
        private string _exportedCode = null!;
        private byte[] _expectedDefaultEncordingCode = null!;
        private byte[] _expectedUtf32EncordingCode = null!;

        [TestInitialize]
        public void InitializeForExportToTest()
        {
            _exportToTestTarget.AddContent(new CsUsingDirectiveContent("System"));
            _exportToTestTarget.AddContent(new CsUsingDirectiveContent("Microsoft.VisualStudio.TestTools.UnitTesting.Logging"));
            _exportToTestTarget.AddContent(new CsLineContent());

            CsNamespaceContent content = new("Test1");
            content.AddContent(new CsClassContent("Content1"));
            content.AddContent(new CsNamespaceContent("Content2"));
            _exportToTestTarget.AddContent(content);
            _exportToTestTarget.AddContent(new CsNamespaceContent("Test2"));

            _exportedCode = _exportToTestTarget.Export();
            _expectedDefaultEncordingCode = Encoding.Default.GetBytes(_exportedCode);
            _expectedUtf32EncordingCode = Encoding.UTF32.GetBytes(_exportedCode);
        }

        [TestMethod]
        public void ClearContentsTest()
        {
            CsTopLevelContent content = new();
            CsLineContent line1 = new();
            CsLineContent line2 = new();
            content.AddContent(line1);
            content.AddContent(line2);

            Assert.AreEqual(2, content.Contents.Count);
            content.ClearContents();
            Assert.AreEqual(0, content.Contents.Count);
        }

        [TestMethod]
        public void ExportAsStringTest()
        {
            CodeHelper.TabSize = 1;
            string expectedCode =
@"using System;
using Microsoft.VisualStudio.TestTools.UnitTesting.Logging;

namespace Test1
{
 class Content1
 {
 }
 namespace Content2
 {
 }
}
namespace Test2
{
}
";

            Assert.AreEqual(expectedCode, _exportToTestTarget.Export());
        }

        [TestMethod]
        public void ExportToStreamTest()
        {
            using var ms = new MemoryStream();
            _exportToTestTarget.ExportTo(ms);
            CollectionAssert.AreEqual(_expectedDefaultEncordingCode, ms.ToArray());
        }

        [TestMethod]
        public void ExportToStreamEncordingTest()
        {
            using var ms = new MemoryStream();
            _exportToTestTarget.ExportTo(ms, Encoding.UTF32);
            CollectionAssert.AreEqual(_expectedUtf32EncordingCode, ms.ToArray());
        }

        [TestMethod]
        public void ExportToStreamWriterTest()
        {
            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms);
            
            _exportToTestTarget.ExportTo(writer);
            writer.Flush();

            CollectionAssert.AreEqual(_expectedDefaultEncordingCode, ms.ToArray());
        }

        [TestMethod]
        [Timeout(100)]
        [ExecutionTimeTest]
        public void ExportExecutionTimeTest()
        {
            for (int i = 0; i < 30000; i++)
            {
                _ = _exportToTestTarget.Export();
            }
        }

        [TestMethod]
        [Timeout(100)]
        [Priority(10)]
        [ExecutionTimeTest]
        public void ExportToStreamExecutionTimeTest()
        {
            using var ms = new MemoryStream();
            for (int i = 0; i < 30000; i++)
            {
                _exportToTestTarget.ExportTo(ms, Encoding.Default);
            }
        }

        [TestMethod]
        [Timeout(100)]
        [ExecutionTimeTest]
        public void ExportToStreamWriterExecutionTimeTest()
        {
            using var ms = new MemoryStream();
            using var writer = new StreamWriter(ms);
            for (int i = 0; i < 30000; i++)
            {
                _exportToTestTarget.ExportTo(writer);
            }
        }
    }
}


