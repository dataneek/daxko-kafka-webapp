namespace WebApp.Pages.Members
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Text;
    using MediatR;
    using Models;

    public class CreatedChangesetHandler : INotificationHandler<MemberEvent.Created>
    {
        private readonly AppDbContext context;

        public CreatedChangesetHandler(AppDbContext context)
        {
            this.context = context;
        }

        void INotificationHandler<MemberEvent.Created>.Handle(MemberEvent.Created notification)
        {
            var changeset = new MemberChangeset(notification.Member, MemberChangesetMode.Create);

            context.MemberChangesets.Add(changeset);
            context.SaveChanges();
        }
    }
}