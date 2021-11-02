
namespace BuildSoft.Code.Content
{
    public interface IContentController<in TContent> 
        where TContent : ICodeContent<TContent>
    {
        void Add(TContent item);
        void Clear();
        bool Contains(TContent item);
        int IndexOf(TContent item);
        void Insert(int index, TContent item);
        bool Remove(TContent item);
    }
}