namespace BuildSoft.Code.CsGenerator.Content
{
    public interface IAvailable<T> where T : CsContent
    {
        void AddContent(T content);
        bool RemoveContent(T content);
    }
}