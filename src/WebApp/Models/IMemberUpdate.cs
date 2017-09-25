namespace WebApp.Models
{
    using System;

    public interface IMemberUpdate
    {
        string Email { get; }
        string FirstName { get; }
        string LastName { get; }
        Gender Gender { get; }
        DateTime Birthdate { get; }
        string Phone { get; }
    }
}