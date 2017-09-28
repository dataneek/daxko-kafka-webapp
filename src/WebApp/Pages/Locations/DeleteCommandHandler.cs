namespace WebApp.Pages.Locations
{
    using System;
    using System.Linq;
    using Microsoft.EntityFrameworkCore;
    using MediatR;
    using Models;
    
    public class DeleteCommandHandler : IRequestHandler<DeleteCommand>
    {
        private readonly AppDbContext context;
        private readonly IMediator mediator;

        public DeleteCommandHandler(AppDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        void IRequestHandler<DeleteCommand>.Handle(DeleteCommand message)
        {
            var location = 
                context.Locations
                    .SingleOrDefault(t=> t.RowId == message.Id);

            location.Delete();
        
            context.SaveChanges();

            foreach (var notification in location.Events.OfType<INotification>())
                mediator.Publish(notification);
        }
    }
}