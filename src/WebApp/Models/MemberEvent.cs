namespace WebApp.Models
{
    using System;

    public class MemberEvent : IDomainEvent
    {
        public MemberEvent(Member member)
        {
            this.Member = member;
        }

        public Member Member { get; private set; }



        public class Created : MemberEvent
        {
            public Created(Member member) : base(member) { }
        }

        public class Updated : MemberEvent
        {
            public Updated(Member member) : base(member) { }
        }

        public class Deleted : MemberEvent
        {
            public Deleted(Member member) : base(member) { }
        }
    }
}