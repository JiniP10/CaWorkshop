using System;
using System.Collections.Generic;
using System.Text;

namespace CaWorkshop.Domain.Common;

/// <summary>
/// Represents a domain-specific exception.
/// Use this to signal business rule violations or invalid domain operations.
/// </summary>
public abstract class DomainException(string message) : Exception(message)
{
    // This class can be extended to create specific exceptions within the domain layer.
    // Keeping it abstract ensures that only meaningful, named exceptions are created.
}
