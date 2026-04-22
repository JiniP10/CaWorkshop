using System;
using System.Collections.Generic;
using System.Text;

using Ardalis.GuardClauses;

using CaWorkshop.Application.Common.Models;
using CaWorkshop.Domain.Kanban.Repositories;

using MediatR;

namespace CaWorkshop.Application.Kanban.Queries.ViewBoard;

public record ViewBoardQuery(Guid? BoardId = null) : IRequest<Result<BoardDto>>;

public class ViewBoardQueryHandler : IRequestHandler<ViewBoardQuery, Result<BoardDto>>
{
    private readonly IBoardRepository _repository;

    public ViewBoardQueryHandler(IBoardRepository repository)
    {
        _repository = repository;
    }

    public async Task<Result<BoardDto>> Handle(
        ViewBoardQuery request, CancellationToken cancellationToken)
    {
        var boardId = request.BoardId;

        var board = boardId.HasValue && boardId.Value != Guid.Empty
            ? await _repository.GetByIdAsync(boardId.Value, cancellationToken)
            : await _repository.GetFirstAsync(cancellationToken);

        Guard.Against.NotFound(boardId ?? Guid.Empty, board);

        return board.ToDto();
    }
}
