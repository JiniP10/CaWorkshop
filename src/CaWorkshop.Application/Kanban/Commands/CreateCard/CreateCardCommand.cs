using System;
using System.Collections.Generic;
using System.Text;


using Ardalis.GuardClauses;

using CaWorkshop.Application.Common.Models;
using CaWorkshop.Domain.Common;
using CaWorkshop.Domain.Kanban.Repositories;

using MediatR;

namespace CaWorkshop.Application.Kanban.Commands.CreateCard;

public record CreateCardCommand(
    Guid BoardId,
    Guid ColumnId,
    string Title,
    string Description
) : IRequest<Result<Guid>>;

public class CreateCardCommandHandler : IRequestHandler<CreateCardCommand, Result<Guid>>
{
    private readonly IBoardRepository _boardRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateCardCommandHandler(
        IBoardRepository boardRepository,
        IUnitOfWork unitOfWork)
    {
        _boardRepository = boardRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<Guid>> Handle(CreateCardCommand request, CancellationToken cancellationToken)
    {
        var board = await _boardRepository.GetByIdAsync(request.BoardId, cancellationToken);
        Guard.Against.NotFound(request.BoardId, board);

        var column = board.GetColumnById(request.ColumnId);
        var card = column.AddCard(request.Title, request.Description);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return card.Id; // Implicitly converted to Result<Guid>
    }
}
