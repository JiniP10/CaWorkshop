using CaWorkshop.Application.Kanban.Commands.CreateCard;
using CaWorkshop.WebApi.Kanban.ViewBoard;

using MediatR;

using Microsoft.AspNetCore.Http.HttpResults;

namespace CaWorkshop.WebApi.Kanban.CreateCard;

internal static class CreateCardHandler
{
    [EndpointSummary("Create Card")]
    [EndpointDescription("Creates a new card in the specified column of the specified board.")]
    internal static async Task<Results<Created<Guid>, ValidationProblem>> HandleAsync(
        Guid boardId,
        Guid columnId,
        CreateCardRequest request,
        ISender mediator)
    {
        var result = await mediator.Send(request.ToCommand(boardId, columnId));

        if (result.IsFailure)
            return TypedResults.ValidationProblem(result.Errors);

        var cardId = result.Value;

        return TypedResults.Created($"/api/boards/{boardId}/columns/{columnId}/cards/{cardId}", cardId);
    }
}
