using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BuildSoft.Code.Content.CSharp.Test;

public class CsMemberContentTest
{
    private class CsMemberContentModel : CsMemberContent
    {
        public CsMemberContentModel() : this("identifier", "type", null)
        {

        }
        public CsMemberContentModel(string identifier, CsType type, IEnumerable<string>? modifiers = null)
            : base(identifier, type, modifiers)
        {
        }
        public override Code ToCode(string indent) => throw new NotImplementedException();

        public new bool IsImmutableHeader
        {
            get => base.IsImmutableHeader;
            set => base.IsImmutableHeader = value;
        }
    }

    [Fact]
    public void ConstructorTest()
    {
        CsMemberContentModel content = new("Test", "Type");
        Assert.Equal("Test", content.Identifier.Value);
        Assert.Equal("Type", content.Type.Value);
        Assert.Equal("Type Test", content.Header);
        Assert.Empty(content.Modifiers);
        Assert.True(content.IsImmutableHeader);

        string[] modifiers = new[] { "public", "virtual" };
        content = new("Test", "Type", modifiers);
        Assert.Equal("public virtual Type Test", content.Header);
        Assert.NotSame(modifiers, content.Modifiers);
        Assert.Equal(modifiers, content.Modifiers.ToArray());
    }

    [Fact]
    public void HeaderTest()
    {
        CsMemberContentModel content = new("Test", "Type");

        content.IsImmutableHeader = true;
        Assert.Same(content.Header, content.Header);

        content.IsImmutableHeader = false;
        Assert.NotSame(content.Header, content.Header);
    }

}
