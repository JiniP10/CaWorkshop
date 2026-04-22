using CaWorkshop.Application;
using CaWorkshop.Infrastructure;
using CaWorkshop.WebApi;
using CaWorkshop.WebApi.Common;
using CaWorkshop.WebApi.Kanban;

using Scalar.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddInfrastructure(builder.Configuration)
    .AddApplication();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();
builder.Services.AddHealthChecks();
builder.Services.AddExceptionHandler<CustomExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

// Required to enable the IExceptionHandler<T> you registered earlier.
// This does NOT configure routing to /error like older versions.
app.UseExceptionHandler(_ => { });

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    await app.InitializeDatabaseAsync();
    app.MapOpenApi();
    app.MapScalarApiReference();
    app.Map("/", () => Results.Redirect("/scalar"));
}

app.UseHttpsRedirection();

app.MapHealthChecks("/health");

app.MapBoardsApi("/api/boards");

app.Run();
