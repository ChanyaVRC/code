using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Test
{
    [TestClass]
    public class CodeHelperTest
    {
        [TestInitialize]
        public void Initialize()
        {
            CodeHelper.TabSize = CodeHelper.TabSize;   
        }

        [TestMethod]
        public void CreateOrGetIndentTest()
        {
            int tabSize = CodeHelper.TabSize;

            CodeHelper.TabSize = 0;
            Assert.AreEqual(string.Empty, CodeHelper.CreateOrGetIndent(10));
            CodeHelper.TabSize = 10;
            Assert.AreEqual(string.Empty, CodeHelper.CreateOrGetIndent(0));

            CodeHelper.TabSize = 1;
            for (int j = 1; j <= 11; j++)
            {
                Assert.AreEqual(new string(' ', j), CodeHelper.CreateOrGetIndent(j));
            }

            CodeHelper.TabSize = tabSize;
        }

        [TestMethod]
        [Timeout(35)]
        [Priority(10)]
        [TestCategory("ExecuteTime")]
        public void CreateOrGetIndentExecuteTimeTest()
        {
            for (int i = 0; i < 100; i++)
            {
                for (int j = 0; j < 100; j++)
                {
                    CodeHelper.TabSize = j;

                    for (int k = 0; k < 100; k++)
                    {
                        _ = CodeHelper.CreateOrGetIndent(k);
                    }
                }
            }
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
