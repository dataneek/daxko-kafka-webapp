namespace WebApp.Pages.Members
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using Models;

    public class MemberUpdatedHandler : INotificationHandler<MemberEvent.Updated>
    {
        private readonly AppDbContext context;

        public MemberUpdatedHandler(AppDbContext context)
        {
            this.context = context;
        }

        void INotificationHandler<MemberEvent.Updated>.Handle(MemberEvent.Updated notification)
        {
            
        }
    }
}