using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.CsGenerator
{
    public class CsFileWriter : IDisposable, IAsyncDisposable
    {
        private static readonly FileStreamOptions _options = new FileStreamOptions
        {
            Access = FileAccess.Write,
            BufferSize = 4096,
            Share = FileShare.None,
            Mode = FileMode.Create,
            Options = FileOptions.None,
            PreallocationSize = 1024,
        };
        public const int TabSize = 4;

        private StreamWriter _writer;
        private int _currentIndent = 0;
        
        public int CurrentIndent
        {
            get => _currentIndent;
            set
            {
                if (value < 0)
                {
                    // TODO: Use ThrowHelper
                    throw new ArgumentException();
                }
                _currentIndent = value;
            }
        }

        private static Dictionary<int, string> _spaceCache = new();
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
            _writer.Write(CreateOrGetIndent(_currentIndent));
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
            await _writer.WriteAsync(CreateOrGetIndent(_currentIndent));
        }


        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static string CreateOrGetIndent(int indent)
        {
            if (_spaceCache.ContainsKey(indent))
            {
                return _spaceCache[indent];
            }
            lock (_spaceCache)
            {
                if (_spaceCache.ContainsKey(indent))
                {
                    return _spaceCache[indent];
                }
                string indentContent = new(' ', indent * TabSize);
                _spaceCache.Add(indent, indentContent);
                return indentContent;
            }
        }

        public void Dispose()
        {
            _writer.Dispose();
        }
        public async ValueTask DisposeAsync()
        {
            await _writer.DisposeAsync();
        }
    }
}
