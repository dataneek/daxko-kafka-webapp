namespace WebApp.Pages.Locations
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using MediatR;
    using Models;
    
    public class UpdateCommandHandler : IRequestHandler<UpdateCommand>
    {
        private readonly AppDbContext context;
        private readonly IMediator mediator;

        public UpdateCommandHandler(AppDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        void IRequestHandler<UpdateCommand>.Handle(UpdateCommand message)
        {
            var Location = 
                context.Locations
                    .SingleOrDefault(t=> t.RowId == message.Id);

            Location.Update(message);
        
            context.SaveChanges();

            foreach (var notification in Location.Events.OfType<INotification>())
                mediator.Publish(notification);
        }
    }
}