using System;
using System.Collections.Generic;
using System.Text;

using CaWorkshop.Domain.Common;
using CaWorkshop.Domain.Kanban.ValueObjects;

namespace CaWorkshop.Domain.Kanban.Entities;

public class Card : Entity
{
    public Guid Id { get; private set; }

    public Guid ColumnId { get; private set; }

    public CardTitle Title { get; private set; }

    public string? Description { get; private set; }

    public int Position { get; private set; }

    internal Card(Guid columnId, CardTitle title, string? description = null, int position = 0)
    {
        ColumnId = columnId;
        Title = title ?? throw new ArgumentNullException(nameof(title));
        Description = description;
        Position = position;
    }

    internal void UpdatePosition(int position)
    {
        Position = position;
    }
}
