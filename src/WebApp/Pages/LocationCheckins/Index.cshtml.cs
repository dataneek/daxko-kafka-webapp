namespace WebApp.Pages.LocationCheckins
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Hangfire;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using PaginableCollections;
    using WebApp.Models;
    using Microsoft.EntityFrameworkCore;

    public class IndexModel : PageModel
    {
        private readonly AppDbContext context;
        private readonly IMediator mediator;

        public IndexModel(AppDbContext context, IMediator mediator)
        {
            this.context = context;
            this.mediator = mediator;
        }

        [BindProperty]
        public CreateModel CreateModel { get; set; }
         public IPaginable<LocationCheckin> LocationCheckins {get; set;}

        public void OnGet()
        {
            CreateModel = new CreateModel();
            get_location_checkins();
        }

        public IActionResult OnPostCreateAsync()
        {
            var NumberToCreate = CreateModel.selected_option;

            mediator.Send(new CreateCommand(){NumberToCreate = NumberToCreate});

            CreateModel.message = $"{NumberToCreate} Members Created";            

            get_location_checkins();

            return Page();
        }

        private void get_location_checkins()
        {
            LocationCheckins =  context.LocationCheckin.Include(t => t.Member).Include(t => t.Location).OrderByDescending(t => t.Created).ToPaginable(1, CreateModel.page_size);
            CreateModel.total_count = LocationCheckins.TotalItemCount;
            CreateModel.page_count = LocationCheckins.Count;
        }
    }
}