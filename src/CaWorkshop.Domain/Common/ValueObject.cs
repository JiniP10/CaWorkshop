using System;
using System.Collections.Generic;
using System.Text;

namespace CaWorkshop.Domain.Common;

/// <summary>
/// Base class for Value Objects in the domain.
/// Implements value-based equality and hash code generation.
/// </summary>
public abstract class ValueObject
{
    // Cached hash code to improve performance when used in collections
    private int? _cachedHashCode;

    /// <summary>
    /// Must be implemented by derived classes to return the atomic values
    /// that make up the Value Object's equality.
    /// </summary>
    protected abstract IEnumerable<object?> GetEqualityComponents();

    public override bool Equals(object? obj)
    {
        if (obj is null)
            return false;

        // Ensure the objects are of the same type
        if (obj.GetType() != GetType())
            return false;

        var valueObject = (ValueObject)obj;

        // Compare all equality components for value-based equality
        return GetEqualityComponents()
            .SequenceEqual(valueObject.GetEqualityComponents());
    }

    public override int GetHashCode()
    {
        // Lazily compute and cache the hash code
        if (!_cachedHashCode.HasValue)
        {
            _cachedHashCode = GetEqualityComponents()
                .Aggregate(1, (hash, component) =>
                {
                    unchecked // Allow arithmetic overflow
                    {
                        return hash * 23 + (component?.GetHashCode() ?? 0);
                    }
                });
        }

        return _cachedHashCode.Value;
    }

    // Equality operator
    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        if (left is null && right is null)
            return true;

        if (left is null || right is null)
            return false;

        return left.Equals(right);
    }

    // Inequality operator
    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !(left == right);
    }
}
