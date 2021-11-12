using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp;

public class CsMethodContent : CsMemberContent
{
    private List<CsArgumentDefinition>? _arguments;
    public IReadOnlyCollection<CsArgumentDefinition> Arguments
        => _arguments ??= new List<CsArgumentDefinition>();

    public string ArgumentList => CreateArgumentList();
    protected virtual string CreateArgumentList()
    {
        if (_arguments == null || _arguments.Count <= 0)
        {
            return string.Empty;
        }

        return string.Join(", ", _arguments);
    }

    public CsMethodContent(
        CsIdentifier identifier,
        CsType returnType,
        IEnumerable<string>? modifiers = null,
        IEnumerable<CsArgumentDefinition>? arguments = null)
        : base(identifier, returnType, modifiers)
    {
        if (arguments != null)
        {
            _arguments = new List<CsArgumentDefinition>(arguments);
        }
    }


    public override Code ToCode(string indent)
    {
        string body;
        if (indent.Length == 0)
        {
            body = $"{Header}({ArgumentList})\r\n{{\r\n}}\r\n";
        }
        else
        {
            body =
$@"{indent}{Header}({ArgumentList})
{indent}{{
{indent}}}
";
        }
        int position = body.Length - "}\r\n".Length - indent.Length;
        return Code.CreateCodeWithContents(body, position, true);
    }
}
