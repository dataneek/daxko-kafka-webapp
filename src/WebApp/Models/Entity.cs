namespace WebApp.Models
{
    using System;
    using System.Collections.Generic;
    
    public abstract class Entity 
    {
        public ICollection<IDomainEvent> Events { get; private set; } = new List<IDomainEvent>();

        public DateTime Created { get; protected set; } = DateTime.UtcNow;
        public DateTime LastUpdated { get; protected set; } = DateTime.UtcNow;

        public byte[] Watermark { get; private set; }
        public Guid RowId { get; protected set; } = Guid.NewGuid();


        protected void CaptureEvent(params IDomainEvent[] domainEvents)
        {
            if (this.Events == null)
                this.Events = new List<IDomainEvent>();

            if (domainEvents != null)
            {
                foreach (var domainEvent in domainEvents)
                    this.Events.Add(domainEvent);
            }
        }

        protected void OnCreated(params IDomainEvent[] domainEvents)
        {
            if (domainEvents != null)
                CaptureEvent(domainEvents);
        }

        protected void OnUpdated(params IDomainEvent[] domainEvents)
        {
            if (domainEvents != null)
                CaptureEvent(domainEvents);

            this.LastUpdated = DateTime.UtcNow;
        }
    }
}