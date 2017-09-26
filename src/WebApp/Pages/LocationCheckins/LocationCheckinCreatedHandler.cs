using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using WebApp.Models;

namespace WebApp.Pages.LocationCheckins
{
    public class LocationCheckinCreatedHandler : INotificationHandler<LocationCheckinEvent.Completed>
    {
        private readonly AppDbContext context;

        public LocationCheckinCreatedHandler(AppDbContext context)
        {
            this.context = context;
        }

        void INotificationHandler<LocationCheckinEvent.Completed>.Handle(LocationCheckinEvent.Completed notification)
        {
            
        }
    }
}