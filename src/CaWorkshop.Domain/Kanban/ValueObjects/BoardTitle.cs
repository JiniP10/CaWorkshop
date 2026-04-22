using System;
using System.Collections.Generic;
using System.Text;

using Ardalis.GuardClauses;

using CaWorkshop.Domain.Common;
using CaWorkshop.Domain.Kanban.Guards;

namespace CaWorkshop.Domain.Kanban.ValueObjects;

public class BoardTitle : ValueObject
{
    public string Value { get; }

    private BoardTitle(string value) => Value = value;

    public static BoardTitle Create(string value)
    {
        Guard.Against.InvalidTitle(value, nameof(BoardTitle));

        return new BoardTitle(value);
    }

    protected override IEnumerable<object?> GetEqualityComponents()
    {
        yield return Value;
    }
}
