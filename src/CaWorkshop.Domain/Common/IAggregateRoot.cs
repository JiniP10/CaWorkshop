using System;
using System.Collections.Generic;
using System.Text;

namespace CaWorkshop.Domain.Common;

/// <summary>
/// Marker interface to indicate that an entity is the root of a domain model cluster.
/// This helps with enforcing consistency boundaries and organizing the domain.
/// </summary>
public interface IAggregateRoot
{
    // No members - used purely as a marker to distinguish aggregate roots from other entities.
}
