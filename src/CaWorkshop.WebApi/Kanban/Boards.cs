using CaWorkshop.WebApi.Kanban.CreateCard;
using CaWorkshop.WebApi.Kanban.ViewBoard;

namespace CaWorkshop.WebApi.Kanban;

internal static class Boards
{
    public static RouteGroupBuilder MapBoardsApi(this IEndpointRouteBuilder app, string prefix)
    {
        var group = app.MapGroup(prefix).WithTags(nameof(Boards));

        group.MapPost("/{boardId:guid}/columns/{columnId:guid}/cards", CreateCardHandler.HandleAsync);
        group.MapGet("/{id:guid?}", ViewBoardHandler.HandleAsync);

        return group;
    }
}