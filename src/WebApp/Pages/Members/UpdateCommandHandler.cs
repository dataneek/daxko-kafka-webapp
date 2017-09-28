namespace WebApp.Pages.Members
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
            var member = 
                context.Members
                    .SingleOrDefault(t=> t.RowId == message.Id);

            member.Update(message);
        
            context.SaveChanges();

            foreach (var notification in member.Events.OfType<INotification>())
                mediator.Publish(notification);
        }
    }
}