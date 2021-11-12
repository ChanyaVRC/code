using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BuildSoft.Code.Content.CSharp;

public /*record*/ class CsNamespace : IEquatable<CsNamespace>
{
    public static readonly CsNamespace Global = new(null);

    public ImmutableArray<CsIdentifier> Domain { get; }
    public string? Value { get; }

    public CsNamespace(CsIdentifier identifier)
    {
        Domain = ImmutableArray.Create(identifier);
        Value = identifier.Value;
    }

    public CsNamespace(string? value)
    {
        if (string.IsNullOrEmpty(value))
        {
            Domain = ImmutableArray<CsIdentifier>.Empty;
            Value = null;
        }
        else
        {
            Domain = value.Split('.').Select(x => new CsIdentifier(x)).ToImmutableArray();
            Value = value;
        }
    }

    public CsNamespace(CsNamespace parent, CsIdentifier identifier)
    {
        Domain = parent.Domain.Add(identifier);

        string? value = parent.Value;
        Value = value == null ? identifier.Value : value + '.' + identifier.Value;
    }

    public CsNamespace(CsNamespace parent, string child)
        : this(parent, new CsNamespace(child))
    {

    }

    public CsNamespace(CsNamespace parent, CsNamespace child)
    {
        if (child.Value == null)
        {
            (Domain, Value) = (parent.Domain, parent.Value);
        }
        else if (parent.Value == null)
        {
            (Domain, Value) = (child.Domain, child.Value);
        }
        else
        {
            Domain = parent.Domain.AddRange(child.Domain);
            Value = parent.Value + '.' + child.Value;
        }
    }

    public override string? ToString() => Value;

    public override int GetHashCode()
    {
        return Value == null ? 0 : Value.GetHashCode();
    }

    public override bool Equals(object? obj)
    {
        if (obj is not null && obj is CsNamespace @namespace)
        {
            return Equals(@namespace);
        }
        return false;
    }
    public virtual bool Equals(CsNamespace? obj)
    {
        if (obj is null || GetType() != obj.GetType())
        {
            return false;
        }

        return Value == obj.Value;
    }

    public static implicit operator CsNamespace(string value) => new(value);

    public static bool operator ==(CsNamespace? left, CsNamespace? right)
        => left is null ? right is null : left.Equals(right);
    public static bool operator !=(CsNamespace? left, CsNamespace? right) => !(left == right);
}
