using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using Xunit;

namespace BuildSoft.Code.Content.Test;

public class ContentContainerTest
{
    private class CodeContent : ICodeContent<CodeContent>
    {
        public IReadOnlyList<CodeContent> Contents => throw new NotImplementedException();
        public string Export() => throw new NotImplementedException();
        public void ExportTo(Stream stream, Encoding encoding) => throw new NotImplementedException();
        public void ExportTo(StreamWriter writer) => throw new NotImplementedException();
        public Code ToCode(int indent) => throw new NotImplementedException();
        public Code ToCode(string indent) => throw new NotImplementedException();
    }

    [Fact]
    public void ConstructorTest()
    {
        ContentContainer<CodeContent> container = new();

        Assert.True(container.CanOperate);
        Assert.False(container.IsReadOnly);
        Assert.Empty(container);
    }

    [Fact]
    public void AddTest()
    {
        ContentContainer<CodeContent> container = new();
        CodeContent item = new();

        container.Add(item);
        Assert.Single(container, item);

        container.CanOperate = false;
        Assert.Throws<InvalidOperationException>(() => container.Add(new CodeContent()));
        Assert.Single(container, item);
    }

    [Fact]
    public void RemoveTest()
    {
        ContentContainer<CodeContent> container = new();
        CodeContent item = new();

        container.Add(item);
        Assert.False(container.Remove(new CodeContent()));
        Assert.Single(container);

        Assert.True(container.Remove(item));
        Assert.Empty(container);

        Assert.False(container.Remove(item));
        Assert.Empty(container);
    }

    [Fact]
    public void ClearTest()
    {
        ContentContainer<CodeContent> container = new();

        container.Add(new CodeContent());
        container.Add(new CodeContent());

        container.Clear();
        Assert.Empty(container);

        container.Clear();
        Assert.Empty(container);
    }

    [Fact]
    public void ContainsTest()
    {
        ContentContainer<CodeContent> container = new();
        CodeContent item = new();

        Assert.DoesNotContain(item, container);

        container.Add(item);
        Assert.Contains(item, container);

        container.Remove(item);
        Assert.DoesNotContain(item, container);
    }

    [Fact]
    public void CopyToTest()
    {
        ContentContainer<CodeContent> container = new();
        CodeContent item1 = new();
        CodeContent item2 = new();

        container.Add(item1);
        container.Add(item2);

        CodeContent[] codeContents = new CodeContent[2];
        container.CopyTo(codeContents, 0);

        Assert.Equal(new[] { item1, item2 }, codeContents);
    }

    [Fact]
    public void GetEnumeratorTest()
    {
        ContentContainer<CodeContent> container = new();
        CodeContent item1 = new();
        CodeContent item2 = new();

        container.Add(item1);
        container.Add(item2);

        using var enumerator = container.GetEnumerator();

        Assert.True(enumerator.MoveNext());
        Assert.Same(item1, enumerator.Current);

        Assert.True(enumerator.MoveNext());
        Assert.Same(item2, enumerator.Current);

        Assert.False(enumerator.MoveNext());

        enumerator.Reset();
        Assert.True(enumerator.MoveNext());
    }

    [Fact]
    public void IndexOfTest()
    {
        ContentContainer<CodeContent> container = new();
        CodeContent item1 = new();
        CodeContent item2 = new();

        container.Add(item1);
        Assert.Equal(0, container.IndexOf(item1));
        Assert.Equal(-1, container.IndexOf(item2));

        container.Add(item2);
        Assert.Equal(1, container.IndexOf(item2));
    }

    [Fact]
    public void InsertTest()
    {
        ContentContainer<CodeContent> container = new();
        CodeContent item1 = new();
        CodeContent item2 = new();

        container.Insert(0, item1);
        Assert.Single(container, item1);

        Assert.Throws<ArgumentOutOfRangeException>(() => container.Insert(2, item2));
        Assert.DoesNotContain(item2, container);

        container.Insert(0, item2);
        Assert.Equal(new[] { item2, item1 }, container);
    }

    [Fact]
    public void RemoveAtTest()
    {
        ContentContainer<CodeContent> container = new();
        CodeContent item1 = new();
        CodeContent item2 = new();

        container.Add(item1);
        container.Add(item2);

        container.RemoveAt(0);
        Assert.DoesNotContain(item1, container);
        Assert.Single(container, item2);

        Assert.Throws<ArgumentOutOfRangeException>(() => container.RemoveAt(1));
    }
}
