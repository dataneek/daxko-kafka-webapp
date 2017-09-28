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

    public class DeleteModel : PageModel
    {
        private readonly AppDbContext context;
        private readonly IMediator mediator;

        public DeleteModel(AppDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        [BindProperty]
        public Guid Id { get; set; }

        public IActionResult OnGet(Guid id)
        {
            var member = 
                context.Members.AsNoTracking().SingleOrDefault(t=> t.RowId == id);

            if (member == null)
                return RedirectToPage("/Members/Index");

            Id = id;

            return Page();
        }
       
        public IActionResult OnPostAsync(Guid id)
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            mediator.Send(new DeleteCommand { Id = id });

            return RedirectToPage("/Members/Index");
        }
    }
}