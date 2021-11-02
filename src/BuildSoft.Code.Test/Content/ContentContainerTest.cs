using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildSoft.Code.Content;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Collections;

namespace BuildSoft.Code.Content.Test
{
    [TestClass]
    public class ContentContainerTest
    {
        class CodeContent : ICodeContent<CodeContent>
        {
            public IReadOnlyList<CodeContent> Contents => throw new NotImplementedException();
            public string Export() => throw new NotImplementedException();
            public void ExportTo(Stream stream, Encoding encoding) => throw new NotImplementedException();
            public void ExportTo(StreamWriter writer) => throw new NotImplementedException();
            public Code ToCode(int indent) => throw new NotImplementedException();
        }

        [TestMethod]
        public void ConstructorTest()
        {
            ContentContainer<CodeContent> container = new();

            Assert.IsTrue(container.CanOperate);
            Assert.IsFalse(container.IsReadOnly);
            Assert.AreEqual(0, container.Count);
        }

        [TestMethod]
        public void AddTest()
        {
            ContentContainer<CodeContent> container = new();
            CodeContent item = new();

            container.Add(item);
            Assert.AreEqual(1, container.Count);
            Assert.AreSame(item, container[0]);

            container.CanOperate = false;
            Assert.ThrowsException<InvalidOperationException>(() => container.Add(new CodeContent()));
            Assert.AreEqual(1, container.Count);
        }

        [TestMethod]
        public void RemoveTest()
        {
            ContentContainer<CodeContent> container = new();
            CodeContent item = new();

            container.Add(item);
            Assert.IsFalse(container.Remove(new CodeContent()));
            Assert.AreEqual(1, container.Count);

            Assert.IsTrue(container.Remove(item));
            Assert.AreEqual(0, container.Count);

            Assert.IsFalse(container.Remove(item));
            Assert.AreEqual(0, container.Count);
        }

        [TestMethod]
        public void ClearTest()
        {
            ContentContainer<CodeContent> container = new();
            
            container.Add(new CodeContent());
            container.Add(new CodeContent());
            
            container.Clear();
            Assert.AreEqual(0, container.Count);
            
            container.Clear();
            Assert.AreEqual(0, container.Count);
        }

        [TestMethod]
        public void ContainsTest()
        {
            ContentContainer<CodeContent> container = new();
            CodeContent item = new();
            
            Assert.IsFalse(container.Contains(item));

            container.Add(item);
            Assert.IsTrue(container.Contains(item));

            container.Remove(item);
            Assert.IsFalse(container.Contains(item));
        }

        [TestMethod]
        public void CopyToTest()
        {
            ContentContainer<CodeContent> container = new();
            CodeContent item1 = new();
            CodeContent item2 = new();

            container.Add(item1);
            container.Add(item2);

            CodeContent[] codeContents = new CodeContent[2];
            container.CopyTo(codeContents, 0);

            CollectionAssert.AreEqual(new[] { item1, item2 }, codeContents);
        }

        [TestMethod]
        public void GetEnumeratorTest()
        {
            ContentContainer<CodeContent> container = new();
            CodeContent item1 = new();
            CodeContent item2 = new();

            container.Add(item1);
            container.Add(item2);

            using var enumerator = container.GetEnumerator();

            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreSame(item1, enumerator.Current);
            
            Assert.IsTrue(enumerator.MoveNext());
            Assert.AreSame(item2, enumerator.Current);
            
            Assert.IsFalse(enumerator.MoveNext());

            enumerator.Reset();
            Assert.IsTrue(enumerator.MoveNext());
        }

        [TestMethod]
        public void IndexOfTest()
        {
            ContentContainer<CodeContent> container = new();
            CodeContent item1 = new();
            CodeContent item2 = new();

            container.Add(item1);
            Assert.AreEqual(0, container.IndexOf(item1));
            Assert.AreEqual(-1, container.IndexOf(item2));

            container.Add(item2);
            Assert.AreEqual(1, container.IndexOf(item2));
        }

        [TestMethod]
        public void InsertTest()
        {
            ContentContainer<CodeContent> container = new();
            CodeContent item1 = new();
            CodeContent item2 = new();

            container.Insert(0, item1);
            Assert.AreEqual(1, container.Count);
            Assert.AreSame(item1, container[0]);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => container.Insert(2, item2));
            CollectionAssert.DoesNotContain(container, item2);
            
            container.Insert(0, item2);
            Assert.AreSame(item2, container[0]);
            Assert.AreSame(item1, container[1]);
            Assert.AreEqual(2, container.Count);
        }

        [TestMethod]
        public void RemoveAtTest()
        {
            ContentContainer<CodeContent> container = new();
            CodeContent item1 = new();
            CodeContent item2 = new();

            container.Add(item1);
            container.Add(item2);

            container.RemoveAt(0);
            CollectionAssert.DoesNotContain(container, item1);
            Assert.AreSame(item2, container[0]);
            Assert.AreEqual(1, container.Count);

            Assert.ThrowsException<ArgumentOutOfRangeException>(() => container.RemoveAt(1));
        }
    }
}