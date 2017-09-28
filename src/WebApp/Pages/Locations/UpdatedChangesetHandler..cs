namespace WebApp.Pages.Locations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Text;
    using MediatR;
    using Models;

    public class UpdatedChangesetHandler : INotificationHandler<LocationEvent.Updated>
    {
        private readonly AppDbContext context;

        public UpdatedChangesetHandler(AppDbContext context)
        {
            this.context = context;
        }

        void INotificationHandler<LocationEvent.Updated>.Handle(LocationEvent.Updated notification)
        {
            var changeset = new LocationChangeset(notification.Location, LocationChangesetMode.Update);

            context.LocationChangesets.Add(changeset);
            context.SaveChanges();
        }
    }
}