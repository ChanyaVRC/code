
namespace BuildSoft.Code.Content
{
    public interface ICodeContent<TContent> where TContent : ICodeContent<TContent>
    {
        IReadOnlyList<TContent> Contents { get; }

        string ToCode(out int contentPosition, ref int indent);
    }
}