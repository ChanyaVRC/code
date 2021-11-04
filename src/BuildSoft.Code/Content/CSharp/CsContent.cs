using System.Collections;

namespace BuildSoft.Code.Content.CSharp
{
    public abstract class CsContent : CodeContent<CsContent>, IAvailable<CsLineContent>
    {
        public void AddContent(CsLineContent content)
            => AddableContents.Add(content);
        public bool RemoveContent(CsLineContent content)
            => AddableContents.Remove(content);

        public void ClearContents()
            => AddableContents.Clear();
    }
}