using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.CsGenerator.Content
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

        public override string ToCode(out int contentPosition, ref int currentIndent)
        {
            string oneIndent = CreateIndent(1);
            string body;
            if (currentIndent == 0)
            {
                body = $"namespace {Namespace}\r\n{{\r\n";

                contentPosition = body.Length;

                body += $"\r\n}}\r\n";
            }
            else
            {
                string indentInstance = currentIndent == 1 ? oneIndent : CreateIndent(currentIndent);

                body =
                    $"{indentInstance}namespace {Namespace}\r\n"
                    + $"{indentInstance}{{\r\n";

                contentPosition = body.Length;
                
                body += $"\r\n{indentInstance}}}\r\n";
            }
            currentIndent++;

            return body;
        }
    }
}
