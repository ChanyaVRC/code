using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code;

[AttributeUsage(AttributeTargets.Class, Inherited = true, AllowMultiple = false)]
internal sealed class TestOfAttribute : TestCategoryBaseAttribute
{
    public TestOfAttribute(Type type)
    {
        TargetType = type;
        _testCategories = new List<string>(1) { type.Name + " Test" };
    }
    public Type TargetType { get; }

    private readonly IList<string> _testCategories;
    public override IList<string> TestCategories => _testCategories;
}
