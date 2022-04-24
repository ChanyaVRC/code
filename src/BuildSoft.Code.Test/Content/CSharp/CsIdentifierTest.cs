using System;
using System.Text;
using Xunit;

namespace BuildSoft.Code.Content.CSharp.Test;

public class CsIdentifierTest
{
    [Fact()]
    public void ConstructorTest()
    {
        const int Length = 10;
        for (int i = 0; i < Length; i++)
        {
            StringBuilder sb = new(Length);
            sb.Append('a', Length - 1);
            sb.Insert(i, '*');
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new CsIdentifier(sb.ToString()));
            Assert.Equal("'*' cannot be used at an identifier. (Parameter 'value')", ex.Message);
            Assert.Equal("value", ex.ParamName);
        }
        {
            StringBuilder sb = new(Length);
            sb.Append('a', Length - 1);
            sb.Insert(0, '1');
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new CsIdentifier(sb.ToString()));
            Assert.Equal("'1' cannot be used at the beginning of the identifier. (Parameter 'value')", ex.Message);
            Assert.Equal("value", ex.ParamName);
        }
        for (int i = 1; i < Length; i++)
        {
            StringBuilder sb = new(Length);
            sb.Append('a', Length - 1);
            sb.Insert(i, '1');
            CsIdentifier identifier = new(sb.ToString());
            Assert.Equal(sb.ToString(), identifier.Value);
        }
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new CsIdentifier("a*+b"));
            Assert.Equal("'*' cannot be used at an identifier. (Parameter 'value')", ex.Message);
            Assert.Equal("value", ex.ParamName);
        }
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new CsIdentifier("a＋absc"));
            Assert.Equal("'＋' cannot be used at an identifier. (Parameter 'value')", ex.Message);
            Assert.Equal("value", ex.ParamName);
        }
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new CsIdentifier("0*ab"));
            Assert.Equal("'0' cannot be used at the beginning of the identifier. (Parameter 'value')", ex.Message);
            Assert.Equal("value", ex.ParamName);
        }
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new CsIdentifier("０a"));
            Assert.Equal("'０' cannot be used at the beginning of the identifier. (Parameter 'value')", ex.Message);
            Assert.Equal("value", ex.ParamName);
        }
        {
            ArgumentException ex = Assert.Throws<ArgumentException>(() => new CsIdentifier(""));
            Assert.Equal("Identifier must be at least one character. (Parameter 'value')", ex.Message);
            Assert.Equal("value", ex.ParamName);
        }
    }

    [Fact()]
    public void ToStringTest()
    {
        Assert.Equal("value", new CsIdentifier("value").ToString());
    }
}
