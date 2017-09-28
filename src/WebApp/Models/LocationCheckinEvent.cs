namespace WebApp.Models
{
    using System;
    using MediatR;

    public abstract class LocationCheckinEvent : IDomainEvent, INotification
    {
        public LocationCheckinEvent(LocationCheckin t)
        {
            this.Location = t.Location;
            this.Member = t.Member;
            this.LocationCheckin = t;
            this.CheckinCompleted = t.CheckinCompleted;
        }

        public Member Member { get; private set; }
        public Location Location { get; private set; }
        public LocationCheckin LocationCheckin { get; private set; }
        public DateTime CheckinCompleted { get; private set; }


        public class Created : LocationCheckinEvent
        {
            public Created(LocationCheckin t) : base(t) { }
        }
    }
}