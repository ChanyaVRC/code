using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildSoft.Code.Content.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp.Test;

[TestClass()]
[TestOf(typeof(CsPropertyContent))]
public class CsPropertyContentTest
{
    [TestMethod()]
    public void ConstructorTest()
    {
        CsPropertyContent content = new("Property", "Type");
        Assert.IsNotNull(content);
    }

    [TestMethod()]
    public void ToCodeTest()
    {
        CsPropertyContent content = new("Property", "Type");

        string expectedBody =
@"Type Property
{
}
";
        Code expected = new(expectedBody, expectedBody.Length - "}\r\n".Length, true, true);
        Assert.AreEqual(expected, content.ToCode(""));

        expectedBody =
@" Type Property
 {
 }
";
        expected = new(expectedBody, expectedBody.Length - " }\r\n".Length, true, true);
        Assert.AreEqual(expected, content.ToCode(" "));
    }
}
