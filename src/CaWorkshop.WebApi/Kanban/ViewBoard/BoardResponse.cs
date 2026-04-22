namespace CaWorkshop.WebApi.Kanban.ViewBoard;

public class BoardResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public List<ColumnResponse> Columns { get; set; } = [];
}