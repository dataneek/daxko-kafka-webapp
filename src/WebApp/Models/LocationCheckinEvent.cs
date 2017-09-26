namespace WebApp.Models
{
    using System;
    using MediatR;

    public abstract class LocationCheckinEvent : IDomainEvent, INotification
    {
        public LocationCheckinEvent(LocationCheckin locationCheckin)
        {
            this.Location = locationCheckin.Location;
            this.Member = locationCheckin.Member;
            this.CheckinCompleted = locationCheckin.CheckinCompleted;
        }

        public Member Member { get; private set; }
        public Location Location { get; private set; }
        public DateTime CheckinCompleted { get; private set; }


        public class Completed : LocationCheckinEvent
        {
            public Completed(LocationCheckin locationCheckin) : base(locationCheckin) { }
        }
    }
}