
namespace BuildSoft.Code.Content
{
    public interface ICodeContent<TContent> where TContent : ICodeContent<TContent>
    {
        IReadOnlyList<TContent> Contents { get; }

        public bool IsUsingContent { get; }

        public string ToCode(out int contentPosition, ref int indent);
    }
}