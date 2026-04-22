using CaWorkshop.Application.Kanban.Queries.ViewBoard;

using MediatR;

using Microsoft.AspNetCore.Http.HttpResults;

namespace CaWorkshop.WebApi.Kanban.ViewBoard;

internal static class ViewBoardHandler
{
    [EndpointName(nameof(ViewBoard))]
    [EndpointSummary("Get Board")]
    [EndpointDescription("Retrieves a board by ID, or the default board if no ID is provided.")]
    internal static async Task<Results<Ok<BoardResponse>, NotFound, ValidationProblem>> HandleAsync(
        Guid? id,
        ISender mediator)
    {
        var result = await mediator.Send(new ViewBoardQuery(id));

        if (result.IsFailure)
            return TypedResults.ValidationProblem(result.Errors);

        if (result.Value is null)
            return TypedResults.NotFound();

        return TypedResults.Ok(result.Value.ToResponse());
    }
}