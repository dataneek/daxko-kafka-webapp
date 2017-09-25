namespace WebApp.Pages.Locations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using Models;

    public class LocationCreatedHandler : INotificationHandler<LocationEvent.Created>
    {
        private readonly AppDbContext context;

        public LocationCreatedHandler(AppDbContext context)
        {
            this.context = context;
        }

        void INotificationHandler<LocationEvent.Created>.Handle(LocationEvent.Created notification)
        {
            
        }
    }
}