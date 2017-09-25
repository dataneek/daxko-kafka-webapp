namespace WebApp.Models
{
    using System;

    public class MemberChangeset : IMemberChangeset
    {
        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthdate { get; set; }
    }
}