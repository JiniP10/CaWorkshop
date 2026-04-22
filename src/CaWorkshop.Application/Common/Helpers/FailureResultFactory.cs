using System;
using System.Collections.Generic;
using System.Text;

using System.Reflection;

using CaWorkshop.Application.Common.Models;

namespace CaWorkshop.Application.Common.Helpers;

public static class FailureResultFactory
{
    public static object Create(Type resultType, IDictionary<string, string[]> errors)
    {
        ArgumentNullException.ThrowIfNull(resultType);
        ArgumentNullException.ThrowIfNull(errors);

        // Handle non-generic Result
        if (resultType == typeof(Result))
            return Result.Failure(errors);

        // Handle generic Result<T>
        if (resultType.IsGenericType && resultType.GetGenericTypeDefinition() == typeof(Result<>))
        {
            // Construct the specific Result<T> type (e.g., Result<Guid>)
            var genericResultType = typeof(Result<>).MakeGenericType(resultType.GetGenericArguments());

            // Look for the static method Result<T>.Failure(IDictionary<string, string[]>)
            var failureMethod = genericResultType.GetMethod(
                nameof(Result.Failure),
                BindingFlags.Public | BindingFlags.Static,
                binder: null,
                types: [typeof(IDictionary<string, string[]>)],
                modifiers: null);

            if (failureMethod is not null)
            {
                // Invoke Result<T>.Failure(errors) and return the result
                return failureMethod.Invoke(null, [errors])!;
            }
        }

        // The type was not recognized as Result or Result<T>
        throw new InvalidOperationException(
            $"FailureResultFactory: Unsupported result type '{resultType.FullName}'. Expected Result or Result<T>.");

        // NOTE: For better performance in high-throughput scenarios, consider caching the 
        //       MethodInfo or compiling delegates to avoid repeated reflection.
    }
}