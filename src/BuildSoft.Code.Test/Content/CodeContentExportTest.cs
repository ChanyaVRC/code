using System.IO;
using System.Text;
using BuildSoft.Code.Content.CSharp;
using Xunit;

namespace BuildSoft.Code.Content.Test;

public class CodeContentExportTest
{
    private readonly CsTopLevelContent _exportToTestTarget = new();
    private readonly string _exportedCode;
    private readonly byte[] _expectedDefaultEncordingCode;
    private readonly byte[] _expectedUtf32EncordingCode;

    public CodeContentExportTest()
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

    [Fact]
    public void ClearContentsTest()
    {
        CsTopLevelContent content = new();
        CsLineContent line1 = new();
        CsLineContent line2 = new();
        content.AddContent(line1);
        content.AddContent(line2);

        Assert.Equal(new[] { line1, line2 }, content.Contents);
        content.ClearContents();
        Assert.Empty(content.Contents);
    }

    [Fact]
    public void ExportAsStringTest()
    {
        int oldTabSize = CodeHelper.TabSize;
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

        Assert.Equal(expectedCode, _exportToTestTarget.Export());

        CodeHelper.TabSize = oldTabSize;
    }

    [Fact]
    public void ExportToStreamTest()
    {
        using var ms = new MemoryStream();
        _exportToTestTarget.ExportTo(ms);
        Assert.Equal(_expectedDefaultEncordingCode, ms.ToArray());
    }

    [Fact]
    public void ExportToStreamEncordingTest()
    {
        using var ms = new MemoryStream();
        _exportToTestTarget.ExportTo(ms, Encoding.UTF32);
        Assert.Equal(_exportedCode, Encoding.UTF32.GetString(ms.ToArray()));
        Assert.Equal(_expectedUtf32EncordingCode, ms.ToArray());
    }

    [Fact]
    public void ExportToStreamWriterTest()
    {
        using var ms = new MemoryStream();
        using var writer = new StreamWriter(ms);

        _exportToTestTarget.ExportTo(writer);
        writer.Flush();

        Assert.Equal(_expectedDefaultEncordingCode, ms.ToArray());
    }

    [Fact(Timeout = 100)]
    public void ExportExecutionTimeTest()
    {
        for (int i = 0; i < 30000; i++)
        {
            _ = _exportToTestTarget.Export();
        }
    }

    [Fact(Timeout = 100)]
    public void ExportToStreamExecutionTimeTest()
    {
        using var ms = new MemoryStream();
        for (int i = 0; i < 30000; i++)
        {
            _exportToTestTarget.ExportTo(ms, Encoding.Default);
        }
    }

    [Fact(Timeout = 100)]
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


