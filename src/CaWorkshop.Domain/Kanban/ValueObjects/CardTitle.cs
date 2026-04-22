using System;
using System.Collections.Generic;
using System.Text;

using Ardalis.GuardClauses;

using CaWorkshop.Domain.Common;
using CaWorkshop.Domain.Kanban.Exceptions;
using CaWorkshop.Domain.Kanban.Guards;


namespace CaWorkshop.Domain.Kanban.ValueObjects;

public class CardTitle : ValueObject
{
    public string Value { get; }

    private CardTitle(string value) => Value = value;

    public static CardTitle Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value))
            Guard.Against.InvalidTitle(value, nameof(CardTitle));

        if (value.Length < 3)
            Guard.Against.InvalidTitle(value, nameof(CardTitle));

        if (value.Length > 100)
            Guard.Against.InvalidTitle(value, nameof(CardTitle));

        return new CardTitle(value);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
