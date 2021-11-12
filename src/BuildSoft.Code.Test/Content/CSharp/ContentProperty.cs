using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.Linq;

namespace BuildSoft.Code.Content.CSharp.Test;

[TestClass]
public class ContentProperty
{
    public static List<CsContent> Contents => new()
    {
        new CsTopLevelContent(),
        new CsNamespaceContent("Test"),
        new CsUsingDirectiveContent("System"),
        new CsLineContent(),
    };

    //public static IEnumerable<CsContent> ContentsIsAbleToHaveContents
    //    => Contents.Where(v => !v.GetType().IsSubclassOf(typeof(CsNoContentsContent)));
    //public static IEnumerable<CsContent> ContentsIsNotAbleToHaveContents
    //    => Contents.Where(v => v.GetType().IsSubclassOf(typeof(CsNoContentsContent)));

    [TestMethod]
    [TestCategory("CheckTarget")]
    [Priority(10)]
    [Ignore]
    public void TestTargetCheck()
    {
        var targets = Contents;
        var targetTypes = typeof(CsContent).Assembly.GetTypes()
            .Where(x => x.IsSubclassOf(typeof(CsContent)) && !x.IsAbstract);

        Assert.AreNotSame(targets, Contents);

        foreach (var type in targetTypes)
        {
            Assert.IsTrue(
                targets.Any(v => v.GetType() == type),
                $"{nameof(Contents)} have no {type.Name} instances.");
        }
    }
}
