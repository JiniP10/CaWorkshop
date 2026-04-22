using System;
using System.Collections.Generic;
using System.Text;

namespace CaWorkshop.Application.Common.Behaviors;

using CaWorkshop.Application.Common.Helpers;

using FluentValidation;

using MediatR;
public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var validationResults = await Task.WhenAll(
                _validators.Select(v => v.ValidateAsync(context, cancellationToken)));

            var errors = validationResults
                .SelectMany(r => r.Errors)
                .Where(f => f is not null)
                .GroupBy(f => f.PropertyName, f => f.ErrorMessage)
                .ToDictionary(g => g.Key, g => g.ToArray());

            if (errors.Count != 0)
            {
                // Use exception-based strategy:
                // throw new ValidationException(errors);

                // Use result-based strategy:
                return (TResponse)FailureResultFactory.Create(typeof(TResponse), errors);
            }
        }

        return await next();
    }
}
