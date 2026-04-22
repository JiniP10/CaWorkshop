namespace CaWorkshop.WebApi.Kanban.ViewBoard;

public class CardResponse
{
    public Guid Id { get; set; }

    public string Title { get; set; } = string.Empty;

    public string? Description { get; set; }

    public int Position { get; set; }
}