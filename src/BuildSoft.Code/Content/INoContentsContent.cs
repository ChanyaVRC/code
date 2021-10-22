namespace BuildSoft.Code.Content
{
    internal interface INoContentsContent<TContent> 
        : ICodeContent<TContent> 
        where TContent : ICodeContent<TContent>
    {
        string ToCode(int indent);
    }
}
