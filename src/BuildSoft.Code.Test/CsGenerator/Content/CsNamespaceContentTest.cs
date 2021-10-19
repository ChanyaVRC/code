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
    public class CsNamespaceContentTest
    {
        [TestMethod]
        public void ConstructorTest()
        {
            CsNamespaceContent content = new("Test");
            Assert.AreEqual("Test", content.Namespace);
        }

        [TestMethod]
        public void AddNamespaceContentTest()
        {
            CsNamespaceContent content = new("Parent");
            CsNamespaceContent clildContent = new("Clild");

            Assert.AreEqual(0, content.Contents.Count);

            content.AddContent(clildContent);

            Assert.AreEqual(1, content.Contents.Count);
            Assert.AreSame(clildContent, content.Contents[0]);
        }

        [TestMethod]
        public void RemoveNamespaceContentTest()
        {
            CsNamespaceContent content = new("Parent");
            CsNamespaceContent clildContent1 = new("Child1");
            CsNamespaceContent clildContent2 = new("Child2");

            content.AddContent(clildContent1);
            content.AddContent(clildContent2);

            Assert.AreEqual(2, content.Contents.Count);

            Assert.IsTrue(content.RemoveContent(clildContent2));
            Assert.AreEqual(1, content.Contents.Count);
            Assert.AreSame(clildContent1, content.Contents[0]);

            Assert.IsFalse(content.RemoveContent(clildContent2));
            Assert.AreEqual(1, content.Contents.Count);
            Assert.AreSame(clildContent1, content.Contents[0]);
        }

        [TestMethod]
        public void ToCodeTest()
        {
            CsNamespaceContent content = new("Test");

            int indent = 0;
            string expectedCode = 
@"namespace Test
{

}
";
            int expectedPosition =
@"namespace Test
{
".Length;
            Assert.AreEqual(expectedCode, content.ToCode(out int position, ref indent));
            Assert.AreEqual(expectedPosition, position);
            Assert.AreEqual(1, indent);

            expectedCode = 
@" namespace Test
 {

 }
";
            expectedPosition = 
@" namespace Test
 {
".Length;
            Assert.AreEqual(expectedCode, content.ToCode(out position, ref indent));
            Assert.AreEqual(expectedPosition, position);
            Assert.AreEqual(2, indent);
        }
    }
}