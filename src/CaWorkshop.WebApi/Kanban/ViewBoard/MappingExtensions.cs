using CaWorkshop.Application.Kanban.Commands.CreateCard;
using CaWorkshop.Application.Kanban.Queries.ViewBoard;
using CaWorkshop.WebApi.Kanban.CreateCard;

namespace CaWorkshop.WebApi.Kanban.ViewBoard;

public static class MappingExtensions
{
    public static CardResponse ToResponse(this CardDto dto) => new()
    {
        Id = dto.Id,
        Title = dto.Title,
        Description = dto.Description,
        Position = dto.Position
    };

    public static ColumnResponse ToResponse(this ColumnDto dto) => new()
    {
        Id = dto.Id,
        Title = dto.Title,
        Position = dto.Position,
        Cards = [.. dto.Cards.Select(c => c.ToResponse())]
    };

    public static BoardResponse ToResponse(this BoardDto dto) => new()
    {
        Id = dto.Id,
        Title = dto.Title,
        Columns = [.. dto.Columns.Select(c => c.ToResponse())]
    };


    public static CreateCardCommand ToCommand(this CreateCardRequest request, Guid boardId, Guid columnId) =>
        new CreateCardCommand(
            BoardId: boardId,
            ColumnId: columnId,
            Title: request.Title,
            Description: request.Description
        );
}