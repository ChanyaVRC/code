using BuildSoft.Code.Internal;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using System.Text;

namespace BuildSoft.Code.Content
{
    public abstract class CodeContent<T> : ICodeContent<T> where T: CodeContent<T>
    {
        public IReadOnlyList<T> Contents => AddableContents;

        protected ContentContainer<T> AddableContents => _addableContents ??= new();
        private ContentContainer<T>? _addableContents;
        protected bool HasContents => _addableContents != null && AddableContents.Count != 0;

        public bool CanOperateContents
        {
            get => AddableContents.CanOperate;
            protected set => AddableContents.CanOperate = value;
        }

        public string Export()
        {
            StringBuilder builder = new();
            Stack<(IEnumerator<CodeContent<T>> Enumerator, Code Code)> contentsStack = new();
            int indentCount = 0;
            string indent = string.Empty;
            int tabSize = CodeHelper.TabSize;

            contentsStack.Push((new OneElementEnumerator<CodeContent<T>>(this), Code.Empty));
            while (contentsStack.Count > 0)
            {
                var (enumerator, code) = contentsStack.Pop();
                if (code.NeedsIndent)
                {
                    indentCount--;
                    indent = CodeHelper.CreateOrGetIndentBySpaceCount(indentCount * tabSize);
                }
                if (code.Body.Length - code.ContentsStartIndex > 0)
                {
                    builder.Append(code.Body, code.ContentsStartIndex, code.Body.Length - code.ContentsStartIndex);
                }

                while (enumerator.MoveNext())
                {
                    CodeContent<T> content = enumerator.Current;

                    Code currentCode = content.ToCode(indent);
                    builder.Append(currentCode.Body, 0, currentCode.ContentsStartIndex);

                    if (!currentCode.HasContents)
                    {
                        Debug.Assert(currentCode.Body.Length == currentCode.ContentsStartIndex);
                        continue;
                    }

                    // CsContent.Contents may be before the value is generated.
                    // If the result doesn't have content, CsContent.Contents shouldn't be called.
                    if (content.HasContents)
                    {
                        contentsStack.Push((enumerator, currentCode));
                        enumerator = content.Contents.GetEnumerator();

                        if (currentCode.NeedsIndent)
                        {
                            indentCount++;
                            indent = CodeHelper.CreateOrGetIndentBySpaceCount(indentCount * tabSize);
                        }
                    }
                    else
                    {
                        builder.Append(currentCode.Body, currentCode.ContentsStartIndex, currentCode.Body.Length - currentCode.ContentsStartIndex);
                    }
                }
            }
            Debug.Assert(indentCount == 0);
            return builder.ToString();
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ExportTo(Stream stream)
        {
            ExportTo(stream, Encoding.Default);
        }

        // TODO: make be faster
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public void ExportTo(Stream stream, Encoding encoding)
        {
            ThrowHelper.ThrowIOExceptionIfNullOrCantWrite(stream);

            string exportString = Export();
            byte[] writeBytes = encoding.GetBytes(exportString);
            stream.Write(writeBytes);
        }

        public void ExportTo(StreamWriter writer)
        {
            writer.Write(Export());
        }

        public Code ToCode(int indent) => ToCode(CodeHelper.CreateOrGetIndent(indent));
        public abstract Code ToCode(string indent);
    }
}