using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildSoft.Code.Content.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp.Test
{
    [TestClass]
    [TestOf(typeof(CsMemberContent))]
    public class CsMemberContentTest : CsMemberContent
    {
        #region Members
        public CsMemberContentTest() : this("identifier", "type", null)
        {

        }
        public CsMemberContentTest(string identifier, string type, IEnumerable<string>? modifiers = null)
            : base(identifier, type, modifiers)
        {
        }
        public override Code ToCode(string indent) => throw new NotImplementedException();
        #endregion

        [TestMethod]
        public void ConstructorTest()
        {
            CsMemberContentTest content = new("Test", "Type");
            Assert.AreEqual("Test", content.Identifier.Value);
            Assert.AreEqual("Type", content.Type);
            Assert.AreEqual("Type Test", content.Header);
            Assert.AreEqual(0, content.Modifiers.Count);
            Assert.IsTrue(content.IsImmutableHeader);

            string[] modifiers = new[] { "public", "virtual" };
            content = new("Test", "Type", modifiers);
            Assert.AreEqual("public virtual Type Test", content.Header);
            Assert.AreNotSame(modifiers, content.Modifiers);
            CollectionAssert.AreEqual(modifiers, content.Modifiers.ToArray());
        }

        [TestMethod]
        public void HeaderTest()
        {
            CsMemberContentTest content = new("Test", "Type");

            content.IsImmutableHeader = true;
            Assert.AreSame(content.Header, content.Header);

            content.IsImmutableHeader = false;
            Assert.AreNotSame(content.Header, content.Header);
        }

    }
}