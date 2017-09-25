namespace WebApp.Pages.Members
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using Models;

    public class MemberCreatedHandler : INotificationHandler<MemberEvent.Created>
    {
        private readonly AppDbContext context;

        public MemberCreatedHandler(AppDbContext context)
        {
            this.context = context;
        }

        void INotificationHandler<MemberEvent.Created>.Handle(MemberEvent.Created notification)
        {
            
        }
    }
}