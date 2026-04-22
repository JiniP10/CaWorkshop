using System;
using System.Collections.Generic;
using System.Text;

namespace CaWorkshop.Domain.Kanban.Guards;

using Ardalis.GuardClauses;

using CaWorkshop.Domain.Kanban.Exceptions;

public static class ColumnGuardExtensions
{
    public static void InvalidCardPosition(this IGuardClause guard, int position, int cardCount)
    {
        if (position < 0 || position > cardCount)
        {
            throw new InvalidCardPositionException(position);
        }
    }
}
