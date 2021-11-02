using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace BuildSoft.Code.Test
{
    [TestClass]
    [TestOf(typeof(CodeHelper))]
    public class CodeHelperTest
    {
        [TestInitialize]
        public void Initialize()
        {
            CodeHelper.TabSize = CodeHelper.TabSize;
            ThreadPool.SetMinThreads(1000000, 1000000);
        }

        [TestMethod]
        public void CreateOrGetIndentTest()
        {
            CodeHelper.TabSize = 0;
            Assert.AreEqual(string.Empty, CodeHelper.CreateOrGetIndent(10));
            CodeHelper.TabSize = 10;
            Assert.AreEqual(string.Empty, CodeHelper.CreateOrGetIndent(0));

            CodeHelper.TabSize = 1;
            Assert.AreEqual(new string(' ', 500), CodeHelper.CreateOrGetIndent(500));
            CodeHelper.TabSize = 2;
            Assert.AreEqual(new string(' ', 2), CodeHelper.CreateOrGetIndent(1));
        }

        [TestMethod]
        public void CreateOrGetIndentBySpaceCountTest()
        {
            Assert.AreEqual(new string(' ', 10), CodeHelper.CreateOrGetIndentBySpaceCount(10));
            Assert.AreEqual(new string(' ', 1), CodeHelper.CreateOrGetIndentBySpaceCount(1));
            Assert.AreEqual(string.Empty, CodeHelper.CreateOrGetIndentBySpaceCount(0));
        }

        [TestMethod]
        [Timeout(30)]
        [Priority(10)]
        [ExecutionTimeTest]
        public void CreateOrGetIndentExecutionTimeTest()
        {
            for (int j = 0; j < 10000; j++)
            {
                CodeHelper.TabSize = j % 100;

                for (int k = 0; k < 100; k++)
                {
                    _ = CodeHelper.CreateOrGetIndent(k);
                }
            }
        }

        [TestMethod]
        public void CreateOrGetIndentParallelTest()
        {
            CodeHelper.TabSize = 4;
            Parallel.For(0, 10000, async i =>
            {
                await Task.Delay(1);
                string indent = CodeHelper.CreateOrGetIndent(i);
                Assert.AreEqual(i * 4, indent.Length);
            });
        }

        [TestMethod]
        [Timeout(15)]
        [Priority(10)]
        [ExecutionTimeTest]
        public void CreateOrGetIndentParallelExecutionTimeTest()
        {
            CodeHelper.TabSize = 4;
            Parallel.For(0, 1000000, i => CodeHelper.CreateOrGetIndent(i % 100));
        }

        [TestMethod]
        [ExpectedException(typeof(OverflowException))]
        public void CreateOrGetIndentOverflowTest()
        {
            CodeHelper.TabSize = 2;
            _ = CodeHelper.CreateOrGetIndent(int.MaxValue);
        }
    }
}
