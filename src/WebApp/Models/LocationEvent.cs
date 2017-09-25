namespace WebApp.Models
{
    using System;
    using MediatR;

    public abstract class LocationEvent : IDomainEvent, INotification
    {
        public LocationEvent(Location Location)
        {
            this.Location = Location ?? throw new ArgumentNullException(nameof(Location));
        }

        public Location Location { get; private set; }


        public class Created : LocationEvent
        {
            public Created(Location Location) : base(Location) { }
        }

        public class Updated : LocationEvent
        {
            public Updated(Location Location) : base(Location) { }
        }
    }
}