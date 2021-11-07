using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Diagnostics.CodeAnalysis;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Generator.CSharp
{
    public abstract class CsStatement : IDisposable, IAsyncDisposable, ICsStatement
    {
        public CsFileWriter Writer { get; }
        protected CsStatement? Parent { get; }
        protected CsStatement? Child { get; set; }

        public abstract string HeadKeyword { get; }
        public string Head { get; private set; } = string.Empty;

        public bool IsTopLevelStatement => Parent == null;

        public string CurrentNamespace { get; protected init; }

        protected internal CsStatement(CsFileWriter writer)
        {
            Debug.Assert(writer.CurrentIndent == 0);
            CurrentNamespace = string.Empty;
            Writer = writer;
        }
        protected CsStatement(string head, CsStatement parent)
        {
            CsFileWriter writer = parent.Writer;
            Debug.Assert(writer.CurrentIndent > 0 || (parent.IsTopLevelStatement && writer.CurrentIndent == 0));
            Debug.Assert(parent.Child == null);
            Debug.Assert(parent != this);

            CurrentNamespace = parent.CurrentNamespace;

            Writer = writer;
            Head = head;
            Parent = parent;
            parent.Child = this;

            Writer.AppendLine(head);
            Writer.AppendLine("{");
            Writer.CurrentIndent++;
        }

        public void WriteEmptyLine()
        {
            Writer.AppendLine(string.Empty, false);
        }
        public void WriteLineDynamic(string content, bool appendIndent = true)
        {
            Writer.AppendLine(content, appendIndent);
        }
        public void WriteAttributes(string[] contents, bool isLine = true)
        {
            StringBuilder builder = new(contents.Length * nameof(Attribute).Length);
            builder.Append('[');
            for (int i = 0; i < contents.Length; i++)
            {
                string content = MakeOptimizedAttributeString(contents[i]);

                builder.Append(contents[i]);
                builder.Append(',');
                builder.Append(' ');
            }
            builder.Append(']');

            string attributeString = builder.ToString();
            if (isLine)
            {
                Writer.Append(attributeString);
            }
            else
            {
                Writer.AppendLine(attributeString);
            }
        }

        private static string MakeOptimizedAttributeString(string content)
        {
            if (content.Length > nameof(Attribute).Length && content.EndsWith(nameof(Attribute)))
            {
                // trim string, "Attribute"
                content = content[..^nameof(Attribute).Length];
            }

            return content;
        }

        public void WriteAttribute(Type content, bool isLine = true)
        {
            WriteAttribute(content.Name, isLine);
        }
        public void WriteAttribute(string content, bool isLine = true)
        {
            string attributeString = $"[{MakeOptimizedAttributeString(content)}]";

            if (isLine)
            {
                Writer.Append(attributeString);
            }
            else
            {
                Writer.AppendLine(attributeString);
            }
        }

        private bool _isDisposed = false;
        public virtual void Dispose()
        {
            Debug.Assert(Child == null);
            
            if (!_isDisposed)
            {
                _isDisposed = true;
                if (!IsTopLevelStatement)
                {
                    Debug.Assert(Parent != null);
                    Debug.Assert(Parent.Child == this);
                    Parent.Child = null;

                    Writer.CurrentIndent--;
                    Writer.AppendLine("}");
                }
                GC.SuppressFinalize(this);
            }
        }

        public virtual async ValueTask DisposeAsync()
        {
            Debug.Assert(Child == null);

            if (!_isDisposed)
            {
                _isDisposed = true;

                if (!IsTopLevelStatement)
                {
                    Debug.Assert(Parent != null);
                    Debug.Assert(Parent.Child == this);
                    Parent.Child = null;
                    
                    Writer.CurrentIndent--;
                    await Writer.AppendLineAsync("}");
                }
                GC.SuppressFinalize(this);
            }
        }
    }
}
