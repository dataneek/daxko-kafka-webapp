namespace WebApp.Pages.Locations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using Models;
    using Newtonsoft.Json;

    public class LocationCreatedHandler : INotificationHandler<LocationEvent.Created>
    {
        private readonly AppDbContext context;

        public LocationCreatedHandler(AppDbContext context)
        {
            this.context = context;
        }

        void INotificationHandler<LocationEvent.Created>.Handle(LocationEvent.Created notification)
        {
            var location = notification.Location;
            var changesetContent = JsonConvert.SerializeObject(new Data(location));

            var changeset = new LocationChangeset(
                location, 
                LocationChangeset.LocationChangesetMode.Insert, 
                changesetContent);

            context.LocationChangesets.Add(changeset);
            context.SaveChanges();
        }

        public class Data
        {
            public Data(Location location)
            {
                this.LocationId = location.LocationId;
                this.LocationName = location.LocationName;
                this.Created = location.Created;
                this.LastUpdated = location.LastUpdated;
                this.RowId = location.RowId;
            }

            public int LocationId { get; set; }
            public string LocationName { get; set; }
            public DateTime Created { get; set; }
            public DateTime LastUpdated { get; set; }
            public Guid RowId { get; set; }
        }
    }
}