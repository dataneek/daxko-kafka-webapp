namespace WebApp.Pages.Members
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bogus;
    using Bogus.DataSets;
    using MediatR;
    using WebApp.Models;

    public class CreateCommandHandler : IRequestHandler<CreateCommand>
    {
        private readonly AppDbContext context;
        private readonly IMediator mediator;

        public CreateCommandHandler(AppDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        void IRequestHandler<CreateCommand>.Handle(CreateCommand message)
        {
            var memberUpdates = 
                Enumerable.Range(0, message.NumberToCreate)
                   .Select(t =>
                   {
                       var gender = (t % 2) == 0 ? Name.Gender.Male : Name.Gender.Female;

                       return
                           new Faker<MemberUpdate>()
                               .RuleFor(r => r.FirstName, s => s.Name.FirstName(gender))
                               .RuleFor(r => r.LastName, s => s.Name.LastName(gender))
                               .RuleFor(r => r.Email, (s, e) => s.Internet.Email(e.FirstName, e.LastName))
                               .RuleFor(r => r.Phone, s => s.Phone.PhoneNumber())
                               .RuleFor(r => r.Birthdate, s => s.Person.DateOfBirth)
                               .RuleFor(r => r.Gender, (Gender)gender)
                               .Generate();
                   })
                   .ToArray();

            var members =
                memberUpdates.Select(t => new Member(t));

            context.Members.AddRange(members);                
            context.SaveChanges();
            
            foreach (var notification in members.SelectMany(t => t.Events.OfType<INotification>()))
                mediator.Publish(notification);
        }
    }
}