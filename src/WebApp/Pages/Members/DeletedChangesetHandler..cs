namespace WebApp.Pages.Members
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using System.Text;
    using MediatR;
    using Models;

    public class DeletedChangesetHandler : INotificationHandler<MemberEvent.Deleted>
    {
        private readonly AppDbContext context;

        public DeletedChangesetHandler(AppDbContext context)
        {
            this.context = context;
        }

        void INotificationHandler<MemberEvent.Deleted>.Handle(MemberEvent.Deleted notification)
        {
            var changeset = new MemberChangeset(notification.Member, MemberChangesetMode.Delete);

            context.MemberChangesets.Add(changeset);
            context.SaveChanges();
        }
    }
}