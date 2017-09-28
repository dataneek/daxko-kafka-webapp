namespace WebApp.Pages.Locations
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
            var Location = 
                context.Locations.AsNoTracking().SingleOrDefault(t=> t.RowId == id);

            Input = new InputModel(Location);
        }
       
        public IActionResult OnPostAsync()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            mediator.Send(new UpdateCommand(Input.Id, Input));

            return RedirectToPage("/Locations/Index");
        }


        public class InputModel : ILocationUpdate
        {
            public InputModel() { }
            public InputModel(Location Location)
            {
                this.Id = Location.RowId;
                this.LocationName = Location.LocationName;
            }

            public Guid Id { get; set; }

            [Required]
            public string LocationName { get; set; }
        }
    }
}