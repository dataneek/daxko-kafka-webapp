namespace WebApp.Models
{
    using System;
    using MediatR;

    public abstract class LocationEvent : IDomainEvent, INotification
    {
        public LocationEvent(Location location)
        {
            this.Location = location ?? throw new ArgumentNullException(nameof(location));
        }

        public Location Location { get; private set; }


        public class Created : LocationEvent
        {
            public Created(Location location) : base(location) { }
        }

        public class Updated : LocationEvent
        {
            public Updated(Location location) : base(location) { }
        }

        public class Deleted : LocationEvent
        {
            public Deleted(Location location) : base(location) { }
        }
    }
}