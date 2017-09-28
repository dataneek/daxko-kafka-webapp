namespace WebApp.Models
{
    using System;
    using System.Linq;

    public class LocationData
    {
        public LocationData(Location t)
        {
            RowId = t.RowId;
            LocationName = t.LocationName;
        }

        public Guid RowId { get; set; }
        public string LocationName { get; set; }
    }
}