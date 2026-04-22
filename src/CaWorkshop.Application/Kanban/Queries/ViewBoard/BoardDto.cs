using System;
using System.Collections.Generic;
using System.Text;

namespace CaWorkshop.Application.Kanban.Queries.ViewBoard;

public class BoardDto
{
    public Guid Id { get; set; }
    public string Title { get; set; } = string.Empty;
    public List<ColumnDto> Columns { get; set; } = [];
}