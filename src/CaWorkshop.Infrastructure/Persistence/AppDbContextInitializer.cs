using System;
using System.Collections.Generic;
using System.Text;

using CaWorkshop.Domain.Kanban.Entities;
using CaWorkshop.Domain.Kanban.ValueObjects;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace CaWorkshop.Infrastructure.Persistence;

public interface IAppDbContextInitializer
{
    Task InitializeAsync(CancellationToken cancellationToken = default);
    Task SeedAsync(CancellationToken cancellationToken = default);
}

internal class AppDbContextInitializer : IAppDbContextInitializer
{
    private readonly AppDbContext _context;
    private readonly ILogger _logger;

    public AppDbContextInitializer(AppDbContext context, ILogger<AppDbContextInitializer> logger)
    {
        _context = context;
        _logger = logger;
    }

    public async Task InitializeAsync(CancellationToken cancellationToken = default)
    {
        _logger.LogInformation("Applying database migrations...");
        await _context.Database.MigrateAsync(cancellationToken);
        _logger.LogInformation("Database migrations complete.");
    }

    public async Task SeedAsync(CancellationToken cancellationToken = default)
    {
        if (await _context.Boards.AnyAsync(cancellationToken)) return;

        _logger.LogInformation("Seeding initial data...");

        var board = new Board(BoardTitle.Create("Workshop Board"));

        var planned = board.AddColumn("Planned");
        board.AddColumn("Doing");
        board.AddColumn("Reviewing");
        board.AddColumn("Complete");

        planned.AddCard("Set up project", "Clone the repo and run the build.");
        planned.AddCard("Review domain model", "Understand the Board, Column, and Card structure.");
        planned.AddCard("Implement first use case", "Start with 'Create Board' command.");

        _context.Boards.Add(board);
        await _context.SaveChangesAsync(cancellationToken);

        _logger.LogInformation("Seeding complete.");
    }
}
