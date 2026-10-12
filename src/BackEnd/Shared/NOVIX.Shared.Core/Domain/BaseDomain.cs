using System.Collections.Generic;

namespace NOVIX.Shared.Core.Domain
{
    public abstract class Entity<TId>
    {
        public TId Id { get; protected set; } = default!;

        private readonly List<DomainEvent> _domainEvents = new();
        public IReadOnlyList<DomainEvent> DomainEvents => _domainEvents.AsReadOnly();

        public void AddDomainEvent(DomainEvent domainEvent)
        {
            _domainEvents.Add(domainEvent);
        }

        public void ClearDomainEvents()
        {
            _domainEvents.Clear();
        }
    }

    public abstract class AggregateRoot<TId> : Entity<TId>
    {
    }

    public abstract class DomainEvent
    {
        public System.DateTime OccurredOn { get; protected set; } = System.DateTime.UtcNow;
    }
}
