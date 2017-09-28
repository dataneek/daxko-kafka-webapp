namespace WebApp.Pages.Members
{
    using System;
    using System.Linq;
    using MediatR;
    using Models;

    public class UpdateCommand : IRequest, IMemberUpdate
    {
        public UpdateCommand(Guid id, IMemberUpdate c)
        {
            Id = id;
            FirstName = c.FirstName;
            LastName = c.LastName;
            Phone = c.Phone;
            Email = c.Email;
            Birthdate = c.Birthdate;
            Gender = c.Gender;
        }

        public Guid Id { get; set; }

        public string Email { get; set; }
        public string Phone { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public Gender Gender { get; set; }
        public DateTime Birthdate { get; set; }
    }
}