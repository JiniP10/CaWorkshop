using CaWorkshop.Application.Kanban.Commands.MoveCardCommand;

namespace CaWorkshop.WebApi.Kanban.MoveCard;

public static class MappingExtensions
{
    public static MoveCardCommand ToCommand(this MoveCardRequest request, Guid boardId, Guid columnId, Guid cardId)
    {
        return new MoveCardCommand(boardId, cardId, columnId, request.ToColumn, request.Postion);
    }
}

