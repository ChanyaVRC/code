using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Generator.CSharp;

public class CsFileWriter : IDisposable, IAsyncDisposable
{
    private static readonly FileStreamOptions _options = new()
    {
        Access = FileAccess.Write,
        BufferSize = 4096,
        Share = FileShare.None,
        Mode = FileMode.Create,
        Options = FileOptions.None,
        PreallocationSize = 1024,
    };
    public const int TabSize = 4;

    private readonly StreamWriter _writer;
    private int _currentIndent = 0;

    public int CurrentIndent
    {
        get => _currentIndent;
        set
        {
            ThrowHelper.ThrowArgumentOutOfRangeExceptionIfNegative(value, ParamName.value);
            _currentIndent = value;
        }
    }

    private CsTopLevelStatement? _topLevelStatement;

    public CsFileWriter(string filePath)
    {
        _writer = new StreamWriter(filePath, Encoding.UTF8, _options);
    }

    public CsTopLevelStatement CreateTopLevelStatement()
    {
        return _topLevelStatement ??= new CsTopLevelStatement(this);
    }

    internal void Append(string content)
    {
        _writer.Write(content);
    }
    internal void AppendLine(bool useIndent = false)
    {
        if (useIndent)
        {
            AppendIndent();
        }
        _writer.WriteLine();
    }
    internal void AppendLine(string content, bool useIndent = true)
    {
        if (useIndent)
        {
            AppendIndent();
        }
        _writer.WriteLine(content);
    }
    internal void AppendIndent()
    {
        _writer.Write(CodeHelper.CreateOrGetIndent(_currentIndent));
    }

    internal async Task AppendAsync(string content)
    {
        await _writer.WriteAsync(content);
    }
    internal async Task AppendLineAsync(bool useIndent = false)
    {
        if (useIndent)
        {
            await AppendIndentAsync();
        }
        await _writer.WriteLineAsync();
    }
    internal async Task AppendLineAsync(string content, bool useIndent = true)
    {
        if (useIndent)
        {
            await AppendIndentAsync();
        }
        await _writer.WriteLineAsync(content);
    }
    internal async Task AppendIndentAsync()
    {
        await _writer.WriteAsync(CodeHelper.CreateOrGetIndent(_currentIndent));
    }

    private bool _isDisposed = false;
    public void Dispose()
    {
        if (_isDisposed)
        {
            _isDisposed = true;

            _writer.Dispose();
            GC.SuppressFinalize(this);
        }
    }
    public async ValueTask DisposeAsync()
    {
        if (_isDisposed)
        {
            _isDisposed = true;

            await _writer.DisposeAsync();
            GC.SuppressFinalize(this);
        }
    }
}
