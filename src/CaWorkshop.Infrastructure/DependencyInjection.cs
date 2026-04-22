using System;
using System.Collections.Generic;
using System.Text;

using CaWorkshop.Domain.Common;
using CaWorkshop.Domain.Kanban.Repositories;
using CaWorkshop.Infrastructure.Persistence;
using CaWorkshop.Infrastructure.Persistence.Repositories;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CaWorkshop.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseSqlite(configuration.GetConnectionString("DefaultConnection"), sqliteOptions =>
                sqliteOptions.UseQuerySplittingBehavior(QuerySplittingBehavior.SplitQuery)));

        services.AddScoped<IAppDbContextInitializer, AppDbContextInitializer>();
        services.AddScoped<IBoardRepository, BoardRepository>();
        services.AddScoped<IUnitOfWork, UnitOfWork>();

        return services;
    }
}
