namespace WebApp.Models
{
    using System;

    public interface IMemberChangeset
    {
        string Email { get; }
        string FirstName { get; }
        string LastName { get; }
        Gender Gender { get; }
        DateTime Birthdate { get; }
        string Phone { get; }
    }
}