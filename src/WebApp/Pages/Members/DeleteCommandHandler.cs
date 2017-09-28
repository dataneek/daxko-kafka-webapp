namespace WebApp.Pages.Members
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
            var member = 
                context.Members
                    .SingleOrDefault(t=> t.RowId == message.Id);

            member.Delete();
        
            context.SaveChanges();

            foreach (var notification in member.Events.OfType<INotification>())
                mediator.Publish(notification);
        }
    }
}