using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApp.Models
{
    public class MemberChangeset : Entity
    {
        public MemberChangeset(Member member, MemberChangesetMode changesetMode, string changeset)
        {
            this.Member = member;
            this.Changeset = changeset;
            this.ChangesetMode = changesetMode;
        }

        private MemberChangeset() { }


        public int MemberChangesetId { get; private set; }
        public int MemberId { get; private set; }
        public Member Member { get; private set; }

        public MemberChangesetMode ChangesetMode { get; private set; }
        public string Changeset { get; private set; }


        public enum MemberChangesetMode
        {
            Insert = 0,
            Update = 1,
            Delete = 2
        }
    }
}