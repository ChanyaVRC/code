using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace BuildSoft.Code.Test;

public class CodeHelperTest : IDisposable
{
    private readonly int _oldTabSize;

    public CodeHelperTest()
    {
        _oldTabSize = CodeHelper.TabSize;
        ThreadPool.SetMinThreads(1000000, 1000000);
    }

    public void Dispose()
    {
        CodeHelper.TabSize = _oldTabSize;
    }

    [Theory]
    [InlineData(0, 10)]
    [InlineData(10, 0)]
    [InlineData(1, 500)]
    [InlineData(2, 10)]
    public void CreateOrGetIndentTest(int tabSize, int indentSize)
    {
        CodeHelper.TabSize = tabSize;
        Assert.Equal(new string(' ', tabSize * indentSize), CodeHelper.CreateOrGetIndent(indentSize));
    }

    [Theory]
    [InlineData(0)]
    [InlineData(1)]
    [InlineData(10)]
    [InlineData(10000000)]
    public void CreateOrGetIndentBySpaceCountTest(int spaceCount)
    {
        Assert.Equal(new string(' ', spaceCount), CodeHelper.CreateOrGetIndentBySpaceCount(spaceCount));
    }

    [Fact(Timeout = 30)]
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

    [Fact]
    public void CreateOrGetIndentParallelTest()
    {
        CodeHelper.TabSize = 4;
        Parallel.For(0, 10000, async i =>
        {
            await Task.Delay(1);
            string indent = CodeHelper.CreateOrGetIndent(i);
            Assert.Equal(i * CodeHelper.TabSize, indent.Length);
        });
    }

    [Fact(Timeout = 15)]
    public void CreateOrGetIndentParallelExecutionTimeTest()
    {
        CodeHelper.TabSize = 4;
        Parallel.For(0, 1000000, i => CodeHelper.CreateOrGetIndent(i % 100));
    }

    [Theory]
    [InlineData(2, 0x40000000)]
    [InlineData(4, 0x20000000)]
    public void CreateOrGetIndentOverflowTest(int tabSize, int indentSize)
    {
        CodeHelper.TabSize = tabSize;
        Assert.Throws<OverflowException>(() => _ = CodeHelper.CreateOrGetIndent(indentSize));
    }
}
