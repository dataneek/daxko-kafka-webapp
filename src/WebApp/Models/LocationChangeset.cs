namespace WebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class LocationChangeset : Entity
    {
        public LocationChangeset(Location location, LocationChangesetMode changesetMode)
        {
            this.LocationId = location.RowId;
            this.Changeset = JsonConvert.SerializeObject(new Data(location));
            this.ChangesetMode = changesetMode;
        }

        private LocationChangeset() { }


        public int LocationChangesetId { get; private set; }
        public Guid LocationId { get; private set; }
        public Location Location { get; private set; }

        public LocationChangesetMode ChangesetMode { get; private set; }
        public string Changeset { get; private set; }


        public class Data
        {
            public Data(Location location)
            {
                LocationName = location.LocationName;
                Id = location.RowId;
            }

            public string LocationName { get; set; }
            public Guid Id { get; set; }
        }
    }
}