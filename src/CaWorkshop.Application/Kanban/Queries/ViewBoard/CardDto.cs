using System;
using System.Collections.Generic;
using System.Text;

namespace CaWorkshop.Application.Kanban.Queries.ViewBoard;

public class CardDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public string? Description { get; set; } = string.Empty;
    public int Position { get; set; }
}
