namespace BuildSoft.Code.Content;

public interface IAvailable<T>
{
    void AddContent(T content);
    bool RemoveContent(T content);
}
