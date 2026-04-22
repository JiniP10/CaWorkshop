namespace CaWorkshop.WebApi.Kanban.MoveCard;

public class MoveCardRequest
{
    public Guid ToColumn { get; set; }
    public int Postion { get; set; }
}