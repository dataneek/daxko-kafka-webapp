namespace WebApp.Pages.Locations
{
    using System;
    using System.Linq;
    using MediatR;
    using Models;

    public class DeleteCommand : IRequest
    {
        public Guid Id { get; set; }
    }
}