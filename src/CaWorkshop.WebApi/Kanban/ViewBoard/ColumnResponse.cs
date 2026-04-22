namespace CaWorkshop.WebApi.Kanban.ViewBoard;

public class ColumnResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public int Position { get; set; }

    public List<CardResponse> Cards { get; set; } = [];
}