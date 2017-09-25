namespace WebApp.Models
{
    public class Location : Entity
    {
        public Location(ILocationUpdate c)
        {
            UpdateFromChangeset(c);
            OnCreated(new LocationEvent.Created(this));
        }

        private Location() { }


        public int LocationId { get; private set; }
        public string LocationName { get; private set; }


        public void Update(ILocationUpdate c)
        {
            UpdateFromChangeset(c);
            OnUpdated(new LocationEvent.Updated(this));
        }

        private void UpdateFromChangeset(ILocationUpdate c)
        {
            LocationName = c.LocationName;
        }
    }
}