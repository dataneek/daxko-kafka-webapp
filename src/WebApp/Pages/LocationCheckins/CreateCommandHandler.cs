using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using MediatR;
using WebApp.Models;

namespace WebApp.Pages.LocationCheckins
{
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
            var members = context.Members.ToList();
            var locations = context.Locations.ToList();

            var locationCheckinUpdates = Enumerable.Range(0, message.NumberToCreate)
                   .Select(t =>
                   {
                       return
                           new Faker<LocationCheckin>()
                               .RuleFor(r => r.Member, s => s.PickRandom(members))
                               .RuleFor(r => r.Location, s => s.PickRandom(locations))       
                               .RuleFor(r => r.CheckinCompleted, s => s.Date.Between(DateTime.Now.AddYears(-1), DateTime.Now))
                               .Generate();
                   })
                   .ToArray();

            var LocationCheckins = locationCheckinUpdates.Select( t => new LocationCheckin(t.Location, t.Member, t.CheckinCompleted));
            
            context.LocationCheckin.AddRange(LocationCheckins);

            foreach (var notification in LocationCheckins.SelectMany(t => t.Events.OfType<INotification>()))
                mediator.Publish(notification);
                
            context.SaveChanges();
        }
    }
}