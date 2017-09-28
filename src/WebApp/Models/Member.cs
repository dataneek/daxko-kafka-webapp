namespace WebApp.Models
{
    using System;
    using System.Collections.Generic;

    public class Member : Entity, IDeletable
    {
        public Member(IMemberUpdate c)
        {
            UpdateFromChangeset(c);
            OnCreated(new MemberEvent.Created(this));
        }

        private Member() { }

        public int MemberId { get; private set; }
        public string Email { get; private set; }
        public string Phone { get; private set; }
        public string FirstName { get; private set; }
        public string LastName { get; private set; }
        public Gender Gender { get; private set; } = Gender.NotSpecified;
        public DateTime Birthdate { get; private set; }
        public bool IsDeleted { get; private set; }


        public void Update(IMemberUpdate c)
        {
            UpdateFromChangeset(c);
            OnUpdated(new MemberEvent.Updated(this));
        }

        private void UpdateFromChangeset(IMemberUpdate c)
        {
            Email = c.Email;
            FirstName = c.FirstName;
            LastName = c.LastName;
            Gender = c.Gender;
            Birthdate = c.Birthdate;
            Phone = c.Phone;
        }

        public void Delete()
        {
            IsDeleted = true;
            OnUpdated(new MemberEvent.Updated(this), new MemberEvent.Deleted(this));
        }
    }
}