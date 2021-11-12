using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;

namespace BuildSoft.Code;

[AttributeUsage(AttributeTargets.Method, AllowMultiple = false)]
internal sealed class ExecutionTimeTestAttribute : TestCategoryBaseAttribute
{
    public ExecutionTimeTestAttribute()
    {
    }

    private static readonly List<string> _testCategories = new(1) { "Execution Time Test" };
    public override IList<string> TestCategories => _testCategories;
}
