namespace WebApp.Models
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Newtonsoft.Json;

    public class MemberChangeset : Entity
    {
        public MemberChangeset(Member member, MemberChangesetMode changesetMode)
        {
            this.MemberId = member.RowId;
            this.Changeset = JsonConvert.SerializeObject(new Data(member));
            this.ChangesetMode = changesetMode;
        }

        private MemberChangeset() { }


        public int MemberChangesetId { get; private set; }
        public Guid MemberId { get; private set; }
        public MemberChangesetMode ChangesetMode { get; private set; }
        public string Changeset { get; private set; }


        public class Data
        {
            public Data(Member member)
            {
                this.MemberId = member.MemberId;
                this.FirstName = member.FirstName;
                this.LastName = member.LastName;
                this.Phone = member.Phone;
                this.Email = member.Email;
                this.Birthdate = member.Birthdate;
                this.Gender = member.Gender;
            }

            public int MemberId { get; set; }
            public string Email { get; set; }
            public string Phone { get; set; }
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public Gender Gender { get; set; }
            public DateTime Birthdate { get; set; }
        }
    }
}