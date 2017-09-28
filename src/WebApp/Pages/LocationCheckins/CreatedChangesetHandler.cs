namespace WebApp.Pages.LocationCheckins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using Models;

    public class CreatedChangesetHandler : INotificationHandler<LocationCheckinEvent.Created>
    {
        private readonly AppDbContext context;

        public CreatedChangesetHandler(AppDbContext context)
        {
            this.context = context;
        }

        void INotificationHandler<LocationCheckinEvent.Created>.Handle(LocationCheckinEvent.Created notification)
        {

        }
    }
}