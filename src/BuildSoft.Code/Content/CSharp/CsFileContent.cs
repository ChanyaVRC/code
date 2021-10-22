using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public class CsFileContent 
        : CsContent, IAvailable<CsNamespaceContent>, IAvailable<CsUsingContent>
    {

        public CsFileContent()
        {
        }

        public override string ToCode(out int contentPosition, ref int indent)
        {
            contentPosition = 0;
            return "";
        }

        public void AddContent(CsNamespaceContent content)
            => AddableContents.Add(content);
        public void AddContent(CsUsingContent content)
            => AddableContents.Add(content);

        public bool RemoveContent(CsNamespaceContent content)
            => AddableContents.Remove(content);
        public bool RemoveContent(CsUsingContent content)
            => AddableContents.Remove(content);

        #region Export Methods
        public string Export()
        {
            IEnumerator<CsContent> currentEnumerator;
            Stack<(IEnumerator<CsContent> Enumerator, int LastIndex)> contentsStack = new();
            int indent = 0;

            StringBuilder builder = new();

            contentsStack.Push((Enumerable.Repeat(this, 1).GetEnumerator(), 0));
            while (contentsStack.Count > 0)
            {
                (currentEnumerator, int contentLastIndex) = contentsStack.Pop();
                int startIndex = builder.Length - contentLastIndex;

                while (currentEnumerator.MoveNext())
                {
                    CsContent content = currentEnumerator.Current;
                    string code = content.ToCode(out int contentIndex, ref indent);
                    builder.Insert(startIndex, code);

                    IReadOnlyList<CsContent> contents = content.Contents;

                    if (contents.Count > 0)
                    {
                        IEnumerator<CsContent> enumerator = contents.GetEnumerator();
                        int lastIndex = builder.Length - (startIndex + code.Length);

                        contentsStack.Push((currentEnumerator, lastIndex));
                        currentEnumerator = enumerator;

                        startIndex += contentIndex;
                    }
                    else
                    {
                        startIndex += code.Length;
                    }
                }
            }
            return builder.ToString();
        }

        public void Export(Stream stream)
        {
            Export(stream, Encoding.Default);
        }

        public void Export(Stream stream, Encoding encoding)
        {
            if (!stream.CanWrite)
            {
                throw new IOException();
            }
            string exportString = Export();
            byte[] writeBytes = encoding.GetBytes(exportString);
            stream.Write(writeBytes, 0, writeBytes.Length);
        }
        #endregion
    }
}
