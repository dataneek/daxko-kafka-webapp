namespace WebApp.Pages.Locations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using Models;

    public class CreatedChangesetHandler : INotificationHandler<LocationEvent.Created>
    {
        private readonly AppDbContext context;

        public CreatedChangesetHandler(AppDbContext context)
        {
            this.context = context;
        }

        void INotificationHandler<LocationEvent.Created>.Handle(LocationEvent.Created notification)
        {
            var changeset = new LocationChangeset(notification.Location, LocationChangesetMode.Create);

            context.LocationChangesets.Add(changeset);
            context.SaveChanges();
        }
    }
}