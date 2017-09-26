namespace WebApp.Models
{
    using System;

    public class LocationCheckin : Entity
    {
        public LocationCheckin(Location location, Member member)
        {
            this.Member = member;
            this.Location = location;
            this.CheckinCompleted = DateTimeOffset.Now;

            OnCreated(new LocationCheckinEvent.Completed(this));
        }

        public LocationCheckin(Location location, Member member, DateTimeOffset CheckinCompleted)
        {
            this.Member = member;
            this.Location = location;
            this.CheckinCompleted = CheckinCompleted;

            OnCreated(new LocationCheckinEvent.Completed(this));
        }

        private LocationCheckin() { }


        public int LocationCheckinId { get; private set; }
        public int MemberId { get; private set; }
        public Member Member { get; private set; }

        public int LocationId { get; private set; }
        public Location Location { get; private set; }
        public DateTimeOffset CheckinCompleted { get; private set; }
    }
}