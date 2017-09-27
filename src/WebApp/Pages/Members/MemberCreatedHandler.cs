namespace WebApp.Pages.Members
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using Models;
    using Newtonsoft.Json;

    public class MemberCreatedHandler : INotificationHandler<MemberEvent.Created>
    {
        private readonly AppDbContext context;

        public MemberCreatedHandler(AppDbContext context)
        {
            this.context = context;
        }

        void INotificationHandler<MemberEvent.Created>.Handle(MemberEvent.Created notification)
        {
            var member = notification.Member;
            var changesetContent = JsonConvert.SerializeObject(new Data(member));

            var changeset = new MemberChangeset(
                member,
                MemberChangeset.MemberChangesetMode.Insert,
                changesetContent);

            context.MemberChangesets.Add(changeset);
            context.SaveChanges();
        }
        public class Data
        {
            public Data(Member member)
            {
                this.MemberId = member.MemberId;
                this.FirstName = member.FirstName;
                this.LastName = member.LastName;
                this.Phone = member.Phone;
                this.Birthdate = member.Birthdate;
                this.Created = member.Created;
                this.LastUpdated = member.LastUpdated;
                this.IsDeleted = member.IsDeleted;
                this.Gender = member.Gender;
            }

            public int MemberId { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Gender Gender { get; set; }
            public DateTime Birthdate { get; set; }
            public DateTime Created { get; set; }
            public DateTime LastUpdated { get; set; }
            public bool IsDeleted { get; set; }
        }
    }
}