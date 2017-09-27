using System;
using System.Linq;
using System.Net.Http;
using Bogus;
using MediatR;
using WebApp.Models;

namespace WebApp.Api.LocationCheckins
{
    public class CheckinCommandHandler : IRequestHandler<CheckinCommand>
    {
        private readonly AppDbContext context;
        private readonly IMediator mediator;

        public CheckinCommandHandler(AppDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        void IRequestHandler<CheckinCommand>.Handle(CheckinCommand message)
        {
            var member = context.Members.FirstOrDefault(x => x.MemberId == message.MemberId);
            var location = context.Locations.FirstOrDefault(x => x.LocationId == message.LocationId);
            
            if (member == null)
                throw new HttpRequestException("Unable to find member");

            if (location == null)
                throw new HttpRequestException("Unable to find location");
            
            var checkin = new LocationCheckin(new LocationCheckinUpdate
            {
                Member = member,
                Location = location,
                CheckinCompleted = DateTime.UtcNow
            });
            
            context.LocationCheckin.AddRange(checkin);

            foreach (var notification in checkin.Events.OfType<INotification>())
                mediator.Publish(notification);
                
            context.SaveChanges();
        }
    }
}