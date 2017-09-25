namespace WebApp.Models
{
    using System;

    public abstract class LocationEvent : IDomainEvent
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