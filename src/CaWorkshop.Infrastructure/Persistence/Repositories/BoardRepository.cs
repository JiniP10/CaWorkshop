using System;
using System.Collections.Generic;
using System.Text;


using CaWorkshop.Domain.Kanban.Entities;
using CaWorkshop.Domain.Kanban.Repositories;

using Microsoft.EntityFrameworkCore;

namespace CaWorkshop.Infrastructure.Persistence.Repositories;

internal class BoardRepository : IBoardRepository
{
    private readonly AppDbContext _context;

    public BoardRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Board?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default)
    {
        return await _context.Boards
            .Include(b => b.Columns.OrderBy(c => c.Position))
                .ThenInclude(c => c.Cards.OrderBy(c => c.Position))
            .FirstOrDefaultAsync(b => b.Id == id, cancellationToken);
    }

    public Task<Board?> GetFirstAsync(CancellationToken cancellationToken = default)
    {
        return _context.Boards
            .Include(b => b.Columns.OrderBy(c => c.Position))
                .ThenInclude(c => c.Cards.OrderBy(c => c.Position))
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);
    }

    public async Task AddAsync(Board board, CancellationToken cancellationToken = default)
    {
        await _context.Boards.AddAsync(board, cancellationToken);
    }

    public void Update(Board board)
    {
        _context.Boards.Update(board);
    }
}
