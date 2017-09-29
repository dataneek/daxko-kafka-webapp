using System;
using System.Collections.Generic;
using System.Linq;
using Bogus;
using MediatR;
using WebApp.Models;
using WebApp.Core;

namespace WebApp.Pages.LocationCheckins
{
    public class CreateCommandHandler : IRequestHandler<CreateCommand>
    {
        private readonly AppDbContext context;
        private readonly IMediator mediator;
        private readonly IGetRandomMembersTask getRandomMembersTask;
        private readonly IGetRandomLocationsTask getRandomLocationsTask;
        

        public CreateCommandHandler(AppDbContext context, IMediator mediator, 
            IGetRandomMembersTask getRandomMembersTask, IGetRandomLocationsTask getRandomLocationsTask)
        {
            this.context = context;
            this.mediator = mediator;
            this.getRandomMembersTask = getRandomMembersTask;
            this.getRandomLocationsTask = getRandomLocationsTask;
        }

        void IRequestHandler<CreateCommand>.Handle(CreateCommand message)
        {
            var members = context.Members.OrderBy(t=> t.MemberId).ToList();
            var locations = context.Locations.OrderBy(t=> t.LocationId).ToList();

            var locationCheckinUpdates = Enumerable.Range(0, message.NumberToCreate)
                   .Select(t =>
                   {
                       return
                           new Faker<LocationCheckinUpdate>()
                               .RuleFor(r => r.Member, s => s.PickRandom(members))
                               .RuleFor(r => r.Location, s => s.PickRandom(locations))       
                               .RuleFor(r => r.CheckinCompleted, s => s.Date.Between(DateTime.Now.AddHours(-1), DateTime.Now))
                               .Generate();
                   })
                   .ToArray();

            var locationCheckins = 
                locationCheckinUpdates.Select( t => new LocationCheckin(t))
                .ToArray();
            
            context.LocationCheckin.AddRange(locationCheckins);

            foreach (var notification in locationCheckins.SelectMany(t => t.Events.OfType<INotification>()))
                mediator.Publish(notification);
                
            context.SaveChanges();
        }
    }
}