namespace BuildSoft.Code.Content.CSharp;

public abstract class CsPropertyAccessor : CsContent, IAvailable<CsStatementContent>
{
    public abstract string Keyword { get; }
    public bool IsAuto { get; }

    public CsPropertyAccessor(bool isAuto)
    {
        IsAuto = isAuto;
        if (isAuto)
        {
            CanOperateContents = false;
        }
    }

    public override Code ToCode(string indent)
    {
        if (IsAuto)
        {
            return Code.CreateWithNoContents($"{indent}{Keyword};\r\n");
        }
        string body = @$"{indent}{Keyword}
{indent}{{
{indent}}}
";
        return Code.CreateWithContents(body, body.Length - "\r\n}}".Length, true);
    }

    public void AddContent(CsStatementContent content)
        => AddableContents.Add(content);
    public bool RemoveContent(CsStatementContent content)
        => AddableContents.Remove(content);
}
