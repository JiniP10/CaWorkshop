using System;
using System.Collections.Generic;
using System.Text;

namespace CaWorkshop.Domain.Common;

/// <summary>
/// Base class for all entities in the domain.
/// An entity is defined by its unique identity (Id) rather than its attributes.
/// </summary>
public abstract class Entity
{
    public Guid Id { get; private set; }

    private readonly List<IDomainEvent> _domainEvents = [];

    public IReadOnlyCollection<IDomainEvent> DomainEvents => _domainEvents.AsReadOnly();

    protected void RaiseDomainEvent(IDomainEvent eventItem)
    {
        _domainEvents.Add(eventItem);
    }

    public void ClearDomainEvents() => _domainEvents.Clear();
}
