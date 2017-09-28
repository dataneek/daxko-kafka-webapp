namespace WebApp.Pages.Members
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Text;
    using MediatR;
    using Models;

    public class UpdatedChangesetHandler : INotificationHandler<MemberEvent.Updated>
    {
        private readonly AppDbContext context;

        public UpdatedChangesetHandler(AppDbContext context)
        {
            this.context = context;
        }

        void INotificationHandler<MemberEvent.Updated>.Handle(MemberEvent.Updated notification)
        {
            var changeset = new MemberChangeset(notification.Member, MemberChangesetMode.Update);

            context.MemberChangesets.Add(changeset);
            context.SaveChanges();
        }
    }
}