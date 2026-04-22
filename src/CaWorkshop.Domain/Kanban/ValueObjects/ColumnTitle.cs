using System;
using System.Collections.Generic;
using System.Text;

namespace CaWorkshop.Domain.Kanban.ValueObjects;

using Ardalis.GuardClauses;

using CaWorkshop.Domain.Common;
using CaWorkshop.Domain.Kanban.Guards;

public class ColumnTitle : ValueObject
{
    public string Value { get; }

    private ColumnTitle(string value) => Value = value;

    public static ColumnTitle Create(string value)
    {
        Guard.Against.InvalidTitle(value, nameof(ColumnTitle));
        return new ColumnTitle(value);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
