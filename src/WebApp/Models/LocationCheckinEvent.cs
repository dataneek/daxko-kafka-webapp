namespace WebApp.Models
{
    using System;

    public abstract class LocationCheckinEvent : IDomainEvent
    {
        public LocationCheckinEvent(LocationCheckin locationCheckin)
        {
            this.Location = locationCheckin.Location;
            this.Member = locationCheckin.Member;
            this.CheckinCompleted = locationCheckin.CheckinCompleted;
        }

        public Member Member { get; private set; }
        public Location Location { get; private set; }
        public DateTimeOffset CheckinCompleted { get; private set; }


        public class Completed : LocationCheckinEvent
        {
            public Completed(LocationCheckin locationCheckin) : base(locationCheckin) { }
        }
    }
}