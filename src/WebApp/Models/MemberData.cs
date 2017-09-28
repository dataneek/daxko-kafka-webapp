namespace WebApp.Models
{
    using System;
    using System.Linq;

    public class MemberData
    {
        public MemberData(Member member)
        {
            RowId = member.RowId;
            FirstName = member.FirstName;
            LastName = member.LastName;
            Gender = member.Gender;
            Phone = member.Phone;
            Email = member.Email;
            Birthdate = member.Birthdate;
        }

        public Guid RowId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthdate { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
    }
}