using System;
using Xunit;

namespace BuildSoft.Code.Content.CSharp.Test;

public class CsArgumentDefinitionTest
{
    [Fact]
    public void ToOptimizedStringTest()
    {
        CsArgumentDefinition argument = new(new(typeof(int)), new("value"));
        Assert.Equal("int value", argument.ToOptimizedString());

        argument = new(new(typeof(int)), new("value"), "in");
        Assert.Equal("in int value", argument.ToOptimizedString());

        argument = new(new(typeof(Uri)), new("value"), "in");
        Assert.Equal("in System.Uri value", argument.ToOptimizedString());
    }

    [Fact()]
    public void ToStringTest()
    {
        CsArgumentDefinition argument = new(new(typeof(int)), new("value"));
        Assert.Equal("int value", argument.ToString());

        argument = new(new(typeof(int)), new("value"), "in");
        Assert.Equal("in int value", argument.ToString());

        argument = new(new(typeof(Uri)), new("value"), "in");
        Assert.Equal("in System.Uri value", argument.ToString());
    }
}
