using System.Text;

namespace BuildSoft.Code.Content;

public interface ICodeContent<TContent> where TContent : ICodeContent<TContent>
{
    IReadOnlyList<TContent> Contents { get; }

    Code ToCode(int indent);
    Code ToCode(string indent);
    string Export();
    void ExportTo(Stream stream) => ExportTo(stream, Encoding.Default);
    void ExportTo(Stream stream, Encoding encoding);
    void ExportTo(StreamWriter writer);
}
