namespace WebApp.Pages.Locations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Text;
    using MediatR;
    using Models;

    public class DeletedChangesetHandler : INotificationHandler<LocationEvent.Deleted>
    {
        private readonly AppDbContext context;

        public DeletedChangesetHandler(AppDbContext context)
        {
            this.context = context;
        }

        void INotificationHandler<LocationEvent.Deleted>.Handle(LocationEvent.Deleted notification)
        {
            var changeset = new LocationChangeset(notification.Location, LocationChangesetMode.Delete);

            context.LocationChangesets.Add(changeset);
            context.SaveChanges();
        }
    }
}