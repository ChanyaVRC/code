using System.Collections;
using System.Runtime.CompilerServices;

namespace BuildSoft.Code.Content.CSharp
{
    public abstract class CsContent : IAvailable<CsLineContent>, ICodeContent<CsContent>
    {
        public virtual IReadOnlyList<CsContent> Contents => AddableContents;
        protected List<CsContent> AddableContents => _addableContents ??= new List<CsContent>();
        private List<CsContent>? _addableContents;

        public abstract string ToCode(out int contentPosition, ref int indent);

        public virtual void AddContent(CsLineContent content)
            => AddableContents.Add(content);
        public virtual bool RemoveContent(CsLineContent content)
            => AddableContents.Remove(content);

        public virtual void ClearContents()
            => AddableContents.Clear();

        protected static string CreateIndent(int indent) => CodeHelper.CreateOrGetIndent(indent);

    }
}