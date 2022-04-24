using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BuildSoft.Code.Content.CSharp.Test;

public class ContentProperty
{
    public static List<CsContent> Contents => new()
    {
        new CsTopLevelContent(),
        new CsNamespaceContent("Test"),
        new CsUsingDirectiveContent("System"),
        new CsLineContent(),
        new CsClassContent("TypeName"),
        new CsFieldContent("id", "TypeName"),
        new CsGetAccessor(),
        new CsSetAccessor(),
        new CsInitAccessor(),
        new CsMethodContent("id", "Type"),
        new CsPropertyContent("id", "Type"),
        new CsStructureContent("Type"),
    };

    [Fact]
    public void TestTargetCheck()
    {
        var targets = Contents;
        var targetTypes = typeof(CsContent).Assembly.GetTypes()
            .Where(x => x.IsSubclassOf(typeof(CsContent)) && !x.IsAbstract);

        Assert.NotSame(targets, Contents);

        foreach (var type in targetTypes)
        {
            Assert.True(
                targets.Any(v => v.GetType() == type),
                $"{nameof(Contents)} have no {type.Name} instances.");
        }
    }
}
