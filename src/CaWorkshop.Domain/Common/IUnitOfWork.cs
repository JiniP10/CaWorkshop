using System;
using System.Collections.Generic;
using System.Text;

namespace CaWorkshop.Domain.Common;

/// <summary>
/// Abstraction for coordinating and saving changes across multiple repositories.
/// Helps ensure that changes are committed as a single atomic operation.
/// </summary>
public interface IUnitOfWork
{
    /// <summary>
    /// Saves all changes made in the current context.
    /// Returns the number of state entries written to the database.
    /// </summary>
    /// <param name="cancellationToken">Optional cancellation token.</param>
    /// <returns>The number of affected entries.</returns>
    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
