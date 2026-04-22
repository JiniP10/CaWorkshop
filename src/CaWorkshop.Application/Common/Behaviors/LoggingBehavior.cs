using System;
using System.Collections.Generic;
using System.Text;

using System.Text.Json;

using MediatR;

using Microsoft.Extensions.Logging;

namespace CaWorkshop.Application.Common.Behaviors;

public class LoggingBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly ILogger<LoggingBehavior<TRequest, TResponse>> _logger;

    public LoggingBehavior(ILogger<LoggingBehavior<TRequest, TResponse>> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;

        _logger.LogInformation("Handling request: {RequestName}", requestName);

        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            // In production, consider redacting or filtering sensitive data from the request
            var requestJson = JsonSerializer.Serialize(request);

            _logger.LogError(ex,
                "Unhandled exception for request: {RequestName}\nRequest Data: {RequestJson}",
                requestName,
                requestJson);

            throw;
        }
    }
}
