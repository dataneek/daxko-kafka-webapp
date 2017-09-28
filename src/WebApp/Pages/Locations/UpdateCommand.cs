namespace WebApp.Pages.Locations
{
    using System;
    using System.Linq;
    using MediatR;
    using Models;

    public class UpdateCommand : IRequest, ILocationUpdate
    {
        public UpdateCommand(Guid id, ILocationUpdate c)
        {
            Id = id;
            LocationName = c.LocationName;
        }

        public Guid Id { get; set; }

        public string LocationName { get; set; }
    }
}