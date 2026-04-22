using MediatR;

using Microsoft.AspNetCore.Http.HttpResults;

namespace CaWorkshop.WebApi.Kanban.MoveCard;

internal static class MoveCardHandler
{
    [EndpointName(nameof(MoveCard))]
    [EndpointSummary("Move Card")]
    [EndpointDescription("Moves a card to a new index within the specified column of the specified board.")]
    internal static async Task<Results<Ok, ValidationProblem>> HandleAsync(
        Guid boardId,
        Guid columnId,
        Guid cardId,
        MoveCardRequest request,
        ISender mediator)
    {
        var result = await mediator.Send(request.ToCommand(boardId, columnId, cardId));

        if (result.IsFailure)
            return TypedResults.ValidationProblem(result.Errors);

        return TypedResults.Ok();
    }
}