using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp
{
    public class CsNamespaceContent : CsContent, IAvailable<CsNamespaceContent>
    {
        public CsNamespaceContent(string @namespace)
        {
            Namespace = @namespace;
        }

        public string Namespace { get; }

        public void AddContent(CsNamespaceContent content)
            => AddableContents.Add(content);

        public bool RemoveContent(CsNamespaceContent content)
            => AddableContents.Remove(content);

        public void AddContent(CsUserDefinedTypeContent content)
            => AddableContents.Add(content);

        public bool RemoveContent(CsUserDefinedTypeContent content)
            => AddableContents.Remove(content);

        public override Code ToCode(string indent)
        {
            string body;
            int contentPosition;

            if (indent.Length == 0)
            {
                body = $"namespace {Namespace}\r\n{{\r\n}}\r\n";

                contentPosition = body.Length - "}\r\n".Length;
            }
            else
            {
                body =
                    $"{indent}namespace {Namespace}\r\n"
                    + $"{indent}{{\r\n"
                    + $"{indent}}}\r\n";

                contentPosition = body.Length - (indent.Length + "}\r\n".Length);
            }
            return Code.CreateCodeWithContents(body, contentPosition, true);
        }
    }
}
