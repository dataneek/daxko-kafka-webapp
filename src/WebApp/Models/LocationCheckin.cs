namespace WebApp.Models
{
    using System;

    public class LocationCheckin : Entity
    {
        public LocationCheckin(ILocationCheckinUpdate c)
        {
            this.MemberId = c.Member.MemberId;
            this.LocationId = c.Location.LocationId;
            this.CheckinCompleted = c.CheckinCompleted;

            OnCreated(new LocationCheckinEvent.Created(this));
        }

        private LocationCheckin() { }


        public int LocationCheckinId { get; private set; }
        public int MemberId { get; private set; }
        public Member Member { get; private set; }

        public int LocationId { get; private set; }
        public Location Location { get; private set; }
        public DateTime CheckinCompleted { get; private set; }
    }
}