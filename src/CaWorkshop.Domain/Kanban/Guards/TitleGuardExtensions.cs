using System;
using System.Collections.Generic;
using System.Text;

namespace CaWorkshop.Domain.Kanban.Guards;

using Ardalis.GuardClauses;

using CaWorkshop.Domain.Kanban.Exceptions;

public static class TitleGuardExtensions
{
    public static string InvalidTitle(this IGuardClause guard, string input, string parameterName)
    {
        if (string.IsNullOrWhiteSpace(input))
            throw new InvalidTitleException($"{parameterName} cannot be empty.");

        if (input.Length < 3)
            throw new InvalidTitleException($"{parameterName} must be at least 3 characters long.");

        if (input.Length > 100)
            throw new InvalidTitleException($"{parameterName} cannot exceed 100 characters.");

        return input;
    }
}
