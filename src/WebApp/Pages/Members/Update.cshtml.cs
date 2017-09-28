namespace WebApp.Pages.Members
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using PaginableCollections;
    using WebApp.Models;
    using Microsoft.EntityFrameworkCore;
    using System.ComponentModel.DataAnnotations;

    public class UpdateModel : PageModel
    {
        private readonly AppDbContext context;
        private readonly IMediator mediator;

        public UpdateModel(AppDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public void OnGet(Guid id)
        {
            var member = 
                context.Members.AsNoTracking().SingleOrDefault(t=> t.RowId == id);

            Input = new InputModel(member);
        }
       
        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            mediator.Send(new UpdateCommand(Input.Id, Input));

            return RedirectToPage("/Members/Index");
        }


        public class InputModel : IMemberUpdate
        {
            public InputModel() { }
            public InputModel(Member member)
            {
                this.Id = member.RowId;
                this.FirstName = member.FirstName;
                this.LastName = member.LastName;
                this.Phone = member.Phone;
                this.Email = member.Email;
                this.Birthdate = member.Birthdate;
                this.Gender = member.Gender;
            }

            public Guid Id { get; set; }

            [Required]
            public string Email { get; set; }

            [Required]
            public string Phone { get; set; }
            [Required]
            public string FirstName { get; set; }
            [Required]
            public string LastName { get; set; }
            [Required]
            public Gender Gender { get; set; }
            [Required]
            public DateTime Birthdate { get; set; }
        }
    }
}