﻿using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildSoft.Code.Collection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Collection.Test
{
    [TestClass]
    [TestOf(typeof(EnumPairs<>))]
    public class EnumPairsTest
    {
        private readonly Dictionary<Flag, string> _initalDictionary = new()
        {
            { Flag.Flag1, "Flag-1" },
            { Flag.Flag2, "Flag-2" },
            { Flag.Flag3, "Flag-3" },
            { Flag.Flag5, "Flag-5" },
        };

        private readonly Dictionary<HasNoneFlag, string> _hasNoneFlagInitalDictionary = new()
        {
            { HasNoneFlag.None, "None-0" },
            { HasNoneFlag.Flag1, "Flag-1" },
            { HasNoneFlag.Flag2, "Flag-2" },
            { HasNoneFlag.Flag3, "Flag-3" },
            { HasNoneFlag.Flag5, "Flag-5" },
        };

        private enum Flag : int
        {
            Flag1 = 1,
            Flag2 = 2,
            Flag3 = 4,
            Flag4 = 8,
            Flag5 = int.MinValue,
        }
        private enum HasNoneFlag : int
        {
            None = 0,
            Flag1 = 1,
            Flag2 = 2,
            Flag3 = 4,
            Flag4 = 8,
            Flag5 = int.MinValue,
        }

        [TestMethod]
        public void ConstructorTest1()
        {
            EnumPairs<Flag> pairs = new();
            SortedList<Flag, string> expected = new();

            Assert.AreEqual(expected.Capacity, pairs.Capacity);
        }

        [TestMethod]
        public void ConstructorTest2()
        {
            EnumPairs<Flag> pairs = new(1);
            SortedList<Flag, string> expected = new(1);

            Assert.AreEqual(expected.Capacity, pairs.Capacity);
        }

        [TestMethod]
        public void ConstructorTest3()
        {
            EnumPairs<Flag> pairs = new(_initalDictionary);
            var expected = _initalDictionary.OrderBy(x => (ulong)Convert.ToInt32(x.Key)).ToArray();

            CollectionAssert.AreEqual(expected, pairs);
        }

        [TestMethod]
        public void GetStringsTest()
        {
            EnumPairs<Flag> pairs = new(_initalDictionary);
            CollectionAssert.AreEqual(Array.Empty<string>(), pairs.GetStrings(0).ToArray());
            CollectionAssert.AreEqual(new[] { "Flag-1" }, pairs.GetStrings(Flag.Flag1).ToArray());
            CollectionAssert.AreEqual(new[] { "Flag-1", "Flag-3" }, pairs.GetStrings(Flag.Flag1 | Flag.Flag3 | Flag.Flag4).ToArray());
            CollectionAssert.AreEqual(new[] { "Flag-1", "Flag-5" }, pairs.GetStrings(Flag.Flag1 | Flag.Flag5).ToArray());

            EnumPairs<HasNoneFlag> pairs2 = new(_hasNoneFlagInitalDictionary);
            CollectionAssert.AreEqual(new[] { "None-0" }, pairs2.GetStrings(0).ToArray());
            CollectionAssert.AreEqual(new[] { "Flag-1" }, pairs2.GetStrings(HasNoneFlag.Flag1).ToArray());
            CollectionAssert.AreEqual(new[] { "Flag-1", "Flag-3" }, pairs2.GetStrings(HasNoneFlag.Flag1 | HasNoneFlag.Flag3 | HasNoneFlag.Flag4).ToArray());
            CollectionAssert.AreEqual(new[] { "Flag-1", "Flag-5" }, pairs2.GetStrings(HasNoneFlag.Flag1 | HasNoneFlag.Flag5).ToArray());
        }

        [TestMethod]
        public void ConvertToStringTest()
        {
            EnumPairs<Flag> pairs = new(_initalDictionary);
            Assert.AreEqual("", pairs.ConvertToString(0, "/"));
            Assert.AreEqual("Flag-1", pairs.ConvertToString(Flag.Flag1, "/"));
            Assert.AreEqual("Flag-1/Flag-2", pairs.ConvertToString(Flag.Flag1 | Flag.Flag2, "/"));
            Assert.AreEqual("Flag-1, Flag-3", pairs.ConvertToString(Flag.Flag1 | Flag.Flag3, ", "));

            EnumPairs<HasNoneFlag> pairs2 = new(_hasNoneFlagInitalDictionary);
            Assert.AreEqual("None-0", pairs2.ConvertToString(0, "/"));
            Assert.AreEqual("Flag-1", pairs2.ConvertToString(HasNoneFlag.Flag1, "/"));
            Assert.AreEqual("Flag-1/Flag-2", pairs2.ConvertToString(HasNoneFlag.Flag1 | HasNoneFlag.Flag2, "/"));
            Assert.AreEqual("Flag-1, Flag-3", pairs2.ConvertToString(HasNoneFlag.Flag1 | HasNoneFlag.Flag3, ", "));
        }
    }
}