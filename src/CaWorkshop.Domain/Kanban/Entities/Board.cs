using System;
using System.Collections.Generic;
using System.Text;

using Ardalis.GuardClauses;

using CaWorkshop.Domain.Common;
using CaWorkshop.Domain.Kanban.Events;
using CaWorkshop.Domain.Kanban.ValueObjects;

namespace CaWorkshop.Domain.Kanban.Entities;

public class Board : Entity, IAggregateRoot
{
    public BoardTitle Title { get; private set; }

    private readonly List<Column> _columns = [];
    public IReadOnlyCollection<Column> Columns => _columns;

    public Board(BoardTitle title)
    {
        Title = Guard.Against.Null(title);
    }

    public Column GetColumnById(Guid columnId)
    {
        var column = _columns.SingleOrDefault(c => c.Id == columnId);

        Guard.Against.NotFound(columnId, column);

        return column;
    }

    public Column AddColumn(string title)
    {
        var columnTitle = ColumnTitle.Create(title);

        var position = _columns.Count;

        var column = new Column(boardId: Id, columnTitle, position);

        _columns.Add(column);

        return column;
    }

    public void MoveCard(Guid cardId, Guid fromColumnId, Guid toColumnId, int position)
    {
        var fromColumn = GetColumnById(fromColumnId);
        var toColumn = GetColumnById(toColumnId);

        var card = fromColumn.RemoveCard(cardId);

        toColumn.InsertCard(position, card);

        if (fromColumn != toColumn)
        {
            RaiseDomainEvent(new CardMovedEvent(cardId, fromColumnId, toColumnId));
        }
    }
}