using Microsoft.VisualStudio.TestTools.UnitTesting;
using BuildSoft.Code.Content.CSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp.Test
{
    [TestClass()]
    [TestOf(typeof(CsArgumentDefinition))]
    public class CsArgumentDefinitionTest
    {
        [TestMethod()]
        public void ToOptimizedStringTest()
        {
            CsArgumentDefinition argument = new(new(typeof(int)), new("value"));
            Assert.AreEqual("int value", argument.ToOptimizedString());

            argument = new(new(typeof(int)), new("value"), "in");
            Assert.AreEqual("in int value", argument.ToOptimizedString());
        
            argument = new(new(typeof(IntPtr)), new("value"), "in");
            Assert.AreEqual("in System.IntPtr value", argument.ToOptimizedString());
        }

        [TestMethod()]
        public void ToStringTest()
        {
            CsArgumentDefinition argument = new(new(typeof(int)), new("value"));
            Assert.AreEqual("int value", argument.ToString());
            
            argument = new(new(typeof(int)), new("value"), "in");
            Assert.AreEqual("in int value", argument.ToString());

            argument = new(new(typeof(IntPtr)), new("value"), "in");
            Assert.AreEqual("in System.IntPtr value", argument.ToString());
        }
    }
}
