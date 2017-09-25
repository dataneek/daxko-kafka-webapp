namespace WebApp.Models
{
    public class Location : Entity
    {
        public Location(ILocationChangeset c)
        {
            UpdateFromChangeset(c);
            OnCreated(new LocationEvent.Created(this));
        }

        private Location() { }


        public int LocationId { get; private set; }
        public string LocationName { get; private set; }


        public void Update(ILocationChangeset c)
        {
            UpdateFromChangeset(c);
            OnUpdated(new LocationEvent.Updated(this));
        }

        private void UpdateFromChangeset(ILocationChangeset c)
        {
            LocationName = c.LocationName;
        }
    }
}