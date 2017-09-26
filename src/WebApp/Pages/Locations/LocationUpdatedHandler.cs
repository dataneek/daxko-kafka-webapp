namespace WebApp.Pages.Locations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using Models;

    public class LocationUpdatedHandler : INotificationHandler<LocationEvent.Updated>
    {
        private readonly AppDbContext context;

        public LocationUpdatedHandler(AppDbContext context)
        {
            this.context = context;
        }

        void INotificationHandler<LocationEvent.Updated>.Handle(LocationEvent.Updated notification)
        {
            
        }
    }
}