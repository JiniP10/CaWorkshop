using System;
using System.Collections.Generic;
using System.Text;

namespace CaWorkshop.Application.Common.Exceptions;

public class ValidationException : Exception
{
    public IDictionary<string, string[]> Errors { get; }

    public ValidationException()
        : base("One or more validation errors have occurred.")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public ValidationException(IDictionary<string, string[]> errors)
        : this()
    {
        ArgumentNullException.ThrowIfNull(errors);
        Errors = errors;
    }

    public ValidationException(string message, IDictionary<string, string[]> errors)
        : base(message)
    {
        ArgumentNullException.ThrowIfNull(errors);
        Errors = errors;
    }
}
