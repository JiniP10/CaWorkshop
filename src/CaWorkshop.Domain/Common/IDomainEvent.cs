using System;
using System.Collections.Generic;
using System.Text;

namespace CaWorkshop.Domain.Common;

/// <summary>
/// Marker interface for domain events.
/// Used to represent something important that happened within the domain.
/// </summary>
public interface IDomainEvent
{
    // No members - this is a marker interface used to identify domain events.
}