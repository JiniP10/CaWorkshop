using System;
using System.Collections.Generic;
using System.Text;

using Ardalis.GuardClauses;

using CaWorkshop.Application.Common.Models;
using CaWorkshop.Domain.Common;
using CaWorkshop.Domain.Kanban.Repositories;

using MediatR;

namespace CaWorkshop.Application.Kanban.Commands.MoveCardCommand;

public record MoveCardCommand(
    Guid BoardId,
    Guid CardId,
    Guid FromColumnId,
    Guid ToColumnId,
    int Position
) : IRequest<Result>;

public class MoveCardCommandHandler : IRequestHandler<MoveCardCommand, Result>
{
    private readonly IBoardRepository _boardRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MoveCardCommandHandler(IBoardRepository boardRepository, IUnitOfWork unitOfWork)
    {
        _boardRepository = boardRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result> Handle(MoveCardCommand request, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetByIdAsync(request.BoardId, cancellationToken);
        Guard.Against.NotFound(request.BoardId, board);

        board.MoveCard(request.CardId, request.FromColumnId, request.ToColumnId, request.Position);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return Result.Success();
    }
}
