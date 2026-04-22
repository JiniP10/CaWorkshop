using System;
using System.Collections.Generic;
using System.Text;

using CaWorkshop.Domain.Kanban.Entities;

namespace CaWorkshop.Domain.Kanban.Repositories;

public interface IBoardRepository
{
    Task<Board?> GetByIdAsync(Guid id, CancellationToken cancellationToken = default);

    Task<Board?> GetFirstAsync(CancellationToken cancellationToken = default);

    Task AddAsync(Board board, CancellationToken cancellationToken = default);

    void Update(Board board);
}
