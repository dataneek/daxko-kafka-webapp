namespace WebApp.Models
{
    using System;
    using System.Collections.Generic;

    public class Location : Entity, IDeletable
    {
        public Location(ILocationUpdate c)
        {
            UpdateFromChangeset(c);
            OnCreated(new LocationEvent.Created(this));
        }

        private Location() { }


        public int LocationId { get; private set; }
        public string LocationName { get; private set; }
        public bool IsDeleted { get; private set;}


        public void Update(ILocationUpdate c)
        {
            UpdateFromChangeset(c);
            OnUpdated(new LocationEvent.Updated(this));
        }

        public void Delete()
        {
            IsDeleted = true;
            OnUpdated(new LocationEvent.Updated(this), new LocationEvent.Deleted(this));
        }

        private void UpdateFromChangeset(ILocationUpdate c)
        {
            LocationName = c.LocationName;
        }
    }
}