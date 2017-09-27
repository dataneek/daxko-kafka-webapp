using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class LocationChangeset : Entity
    {
        public LocationChangeset(Location location, LocationChangesetMode changesetMode, string changeset)
        {
            this.LocationId = location.LocationId;
            this.Changeset = changeset;
            this.ChangesetMode = changesetMode;
        }

        private LocationChangeset() { }


        public int LocationChangesetId { get; private set; }
        public int LocationId { get; private set; }
        public Location Location { get; private set; }

        public LocationChangesetMode ChangesetMode { get; private set; }
        public string Changeset { get; private set; }


        public enum LocationChangesetMode
        {
            Insert = 0,
            Update = 1,
            Delete = 2
        }
    }
}