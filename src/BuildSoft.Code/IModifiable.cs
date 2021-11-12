
namespace BuildSoft.Code;

internal interface IModifiable
{
    IReadOnlyCollection<string> Modifiers { get; }
}
