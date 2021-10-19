using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace BuildSoft.Code.CsGenerator.Content.Test
{
    [TestClass]
    public class CsContentProtectedMembersTest : CsContent
    {
        public override string ToCode(out int contentPosition, ref int contentIndent) => throw new NotImplementedException();

        [TestInitialize]
        public void Initialize()
        {
            CodeHelper.TabSize = CodeHelper.TabSize;
        }

        [TestMethod]
        public void AddableContentsTest()
        {
            CsLineContent line = new();

            Assert.AreEqual(0, Contents.Count);

            AddableContents.Add(line);

            Assert.AreEqual(1, Contents.Count);
            Assert.AreSame(line, Contents[0]);
        }

        [TestMethod]
        public void CreateIndentTest()
        {
            int tabSize = CodeHelper.TabSize;

            CodeHelper.TabSize = 0;
            Assert.AreEqual(string.Empty, CreateIndent(10));
            CodeHelper.TabSize = 10;
            Assert.AreEqual(string.Empty, CreateIndent(0));

            CodeHelper.TabSize = 1;
            for (int j = 1; j <= 11; j++)
            {
                Assert.AreEqual(new string(' ', j), CreateIndent(j));
            }

            CodeHelper.TabSize = tabSize;
        }
    }
}