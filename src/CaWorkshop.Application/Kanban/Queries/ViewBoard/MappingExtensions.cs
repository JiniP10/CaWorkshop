using System;
using System.Collections.Generic;
using System.Text;

using CaWorkshop.Domain.Kanban.Entities;

namespace CaWorkshop.Application.Kanban.Queries.ViewBoard;

internal static class MappingExtensions
{
    public static CardDto ToDto(this Card card) =>
        new()
        {
            Id = card.Id,
            Title = card.Title.Value,
            Description = card.Description ?? string.Empty,
            Position = card.Position
        };

    public static ColumnDto ToDto(this Column column) =>
        new()
        {
            Id = column.Id,
            Title = column.Title.Value,
            Position = column.Position,
            Cards = column.Cards.Select(c => c.ToDto()).ToList()
        };

    public static BoardDto ToDto(this Board board) =>
        new()
        {
            Id = board.Id,
            Title = board.Title.Value,
            Columns = board.Columns.Select(c => c.ToDto()).ToList()
        };
}
