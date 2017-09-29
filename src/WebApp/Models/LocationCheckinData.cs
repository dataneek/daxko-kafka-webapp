using Bogus;

namespace WebApp.Models
{
    using System;
    using System.Linq;

    public class LocationCheckinData
    {
        public LocationCheckinData() { }
        public LocationCheckinData(LocationCheckin t)
        {
            Id = t.RowId;
            Member = new MemberData { Id = t.Member.RowId, FirstName = t.Member.FirstName, LastName = t.Member.LastName };
            Location = new LocationData { Id = t.Location.RowId, LocationName = t.Location.LocationName };
            CheckinCompleted = t.CheckinCompleted;
            Latitude = new Faker().Address.Latitude();
            Longitude = new Faker().Address.Longitude();
        }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public Guid Id { get; set; }
        public MemberData Member { get; set; }
        public LocationData Location { get; set; }
        public DateTime CheckinCompleted { get; set; }


        public class MemberData
        {
            public Guid Id { get;set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
        }

        public class LocationData
        {
            public Guid Id { get; set; }
            public string LocationName { get; set; }
        }
    }
}