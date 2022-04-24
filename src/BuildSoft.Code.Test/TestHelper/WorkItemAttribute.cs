using System;

namespace BuildSoft.Code.Test;

[AttributeUsage(AttributeTargets.Method, Inherited = false, AllowMultiple = true)]
internal sealed class WorkItemAttribute : Attribute
{
    // This is a positional argument
#pragma warning disable IDE0060 // Remove unused parameter
    public WorkItemAttribute(int id, string? url = null)
#pragma warning restore IDE0060 // Remove unused parameter
    {

    }
}
