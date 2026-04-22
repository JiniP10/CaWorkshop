using CaWorkshop.Infrastructure.Persistence;

namespace CaWorkshop.WebApi;

public static class StartupExtensions
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        app.Logger.LogInformation("Starting database initialization...");

        using var scope = app.Services.CreateScope();
        var initializer = scope.ServiceProvider.GetRequiredService<IAppDbContextInitializer>();

        try
        {
            await initializer.InitializeAsync();
            await initializer.SeedAsync();
        }
        catch (Exception ex)
        {
            app.Logger.LogError(ex, "An error occurred during database initialization.");
            throw;
        }

        app.Logger.LogInformation("Database initialization complete.");
    }
}
