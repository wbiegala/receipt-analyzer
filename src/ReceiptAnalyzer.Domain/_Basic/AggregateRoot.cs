namespace BS.ReceiptAnalyzer.Domain.Basic
{
    public abstract class AggregateRoot : Entity
    {
        /// <summary>
        /// Collection of actual domain events that should be published after processing aggregate
        /// </summary>
        public virtual IReadOnlyCollection<DomainEvent> DomainEvents => _events;

        protected virtual void AddEvent(DomainEvent @event)
        {
            _events.Add(@event);
        }

        /// <summary>
        /// Clears domain events for aggregate. Beware of this
        /// </summary>
        public virtual void ClearEvents()
        {
            _events.Clear();
        }

        private List<DomainEvent> _events = new List<DomainEvent>();
    }
}
