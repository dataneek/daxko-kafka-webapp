namespace WebApp.Pages.Locations
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bogus;
    using MediatR;
    using WebApp.Models;

    public class CreateCommandHandler : IRequestHandler<CreateCommand>
    {
        private readonly AppDbContext context;
        private readonly IMediator mediator;

        public CreateCommandHandler(AppDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        void IRequestHandler<CreateCommand>.Handle(CreateCommand message)
        {
            var locationUpdates = 
                Enumerable.Range(0, message.NumberToCreate)
                   .Select(t =>
                   {
                       return
                           new Faker<LocationUpdate>()
                               .RuleFor(r => r.LocationName, s => s.Company.CompanyName())
                               .Generate();
                   })
                   .ToArray();

            var locations =
                locationUpdates.Select(t => new Location(t));

            context.Locations.AddRange(locations);

            foreach (var notification in locations.SelectMany(t => t.Events.OfType<INotification>()))
                mediator.Publish(notification);
                
            context.SaveChanges();
        }
    }
}