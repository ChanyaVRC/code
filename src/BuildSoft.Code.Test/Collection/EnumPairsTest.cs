using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace BuildSoft.Code.Collection.Test;

public class EnumPairsTest
{
    private static readonly Dictionary<Flag, string> _initalDictionary = new()
    {
        { Flag.Flag1, "Flag-1" },
        { Flag.Flag2, "Flag-2" },
        { Flag.Flag3, "Flag-3" },
        { Flag.Flag5, "Flag-5" },
    };

    private static readonly Dictionary<HasNoneFlag, string> _hasNoneFlagInitalDictionary = new()
    {
        { HasNoneFlag.None, "None-0" },
        { HasNoneFlag.Flag1, "Flag-1" },
        { HasNoneFlag.Flag2, "Flag-2" },
        { HasNoneFlag.Flag3, "Flag-3" },
        { HasNoneFlag.Flag5, "Flag-5" },
    };

    private static readonly KeyValuePair<Flag, string>[] _expected
        = _initalDictionary.OrderBy(x => (ulong)Convert.ToInt32(x.Key)).ToArray();

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

    [Fact]
    public void ConstructorTest1()
    {
        EnumPairs<Flag> pairs = new();

        Assert.Equal(0, pairs.Capacity);
    }

    [Theory]
    [InlineData(0)]
    public void ConstructorTest2(int capacity)
    {
        EnumPairs<Flag> pairs = new(capacity);
        Assert.Equal(capacity, pairs.Capacity);
    }

    [Fact]
    public void ConstructorTest3()
    {
        EnumPairs<Flag> pairs = new(_initalDictionary);
        Assert.Equal(_expected, pairs);
    }

    [Fact]
    public void GetStringsTest1()
    {
        EnumPairs<Flag> pairs = new(_initalDictionary);
        Assert.Equal(Array.Empty<string>(), pairs.GetStrings(0).ToArray());
        Assert.Equal(new[] { "Flag-1" }, pairs.GetStrings(Flag.Flag1).ToArray());
        Assert.Equal(Array.Empty<string>(), pairs.GetStrings(Flag.Flag4).ToArray());
        Assert.Equal(new[] { "Flag-1", "Flag-3" }, pairs.GetStrings(Flag.Flag1 | Flag.Flag3 | Flag.Flag4).ToArray());
        Assert.Equal(new[] { "Flag-1", "Flag-5" }, pairs.GetStrings(Flag.Flag1 | Flag.Flag5).ToArray());
    }

    [Fact]
    public void GetStringsTest2()
    {
        EnumPairs<HasNoneFlag> pairs = new(_hasNoneFlagInitalDictionary);
        Assert.Equal(new[] { "None-0" }, pairs.GetStrings(0).ToArray());
        Assert.Equal(new[] { "Flag-1" }, pairs.GetStrings(HasNoneFlag.Flag1).ToArray());
        Assert.Equal(Array.Empty<string>(), pairs.GetStrings(HasNoneFlag.Flag4).ToArray());
        Assert.Equal(new[] { "Flag-1", "Flag-3" }, pairs.GetStrings(HasNoneFlag.Flag1 | HasNoneFlag.Flag3 | HasNoneFlag.Flag4).ToArray());
        Assert.Equal(new[] { "Flag-1", "Flag-5" }, pairs.GetStrings(HasNoneFlag.Flag1 | HasNoneFlag.Flag5).ToArray());
    }

    [Fact]
    public void ConvertToStringTest1()
    {
        EnumPairs<Flag> pairs = new(_initalDictionary);
        Assert.Equal("", pairs.ConvertToString(0, "/"));
        Assert.Equal("Flag-1", pairs.ConvertToString(Flag.Flag1, "/"));
        Assert.Equal("Flag-1/Flag-2", pairs.ConvertToString(Flag.Flag1 | Flag.Flag2, "/"));
        Assert.Equal("Flag-1, Flag-3", pairs.ConvertToString(Flag.Flag1 | Flag.Flag3, ", "));
    }

    [Fact]
    public void ConvertToStringTest2()
    {
        EnumPairs<HasNoneFlag> pairs = new(_hasNoneFlagInitalDictionary);
        Assert.Equal("None-0", pairs.ConvertToString(0, "/"));
        Assert.Equal("Flag-1", pairs.ConvertToString(HasNoneFlag.Flag1, "/"));
        Assert.Equal("Flag-1/Flag-2", pairs.ConvertToString(HasNoneFlag.Flag1 | HasNoneFlag.Flag2, "/"));
        Assert.Equal("Flag-1, Flag-3", pairs.ConvertToString(HasNoneFlag.Flag1 | HasNoneFlag.Flag3, ", "));
    }

    [Fact(Timeout = 150)]
    public void GetStringsExecutionTimeTest()
    {
        const int ElementCount = 25000;
        EnumPairs<Flag> pairs = new(ElementCount);

        for (int i = ElementCount - 1; i >= 0; i--)
        {
            pairs.Add((Flag)i, "value");
        }
    }
}
