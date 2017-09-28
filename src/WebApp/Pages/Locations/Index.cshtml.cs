namespace WebApp.Pages.Locations
{
    using System.Linq;
    using MediatR;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using Microsoft.EntityFrameworkCore;
    using PaginableCollections;
    using WebApp.Models;

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

        public IPaginable<Location> Locations { get; set; }

        public void OnGet()
        {
            CreateModel = new CreateModel();
            get_locations();
        }

        public IActionResult OnPostCreateAsync()
        {
            var NumberToCreate = CreateModel.selected_option;

            mediator.Send(new CreateCommand(){NumberToCreate = NumberToCreate});

            CreateModel.message = $"{NumberToCreate} Locations Created";            

            get_locations();

            return Page();
        }

        private void get_locations()
        {
            Locations = 
                context.Locations
                    .AsNoTracking()
                    .OrderByDescending(t => t.Created)
                    .ToPaginable(1, CreateModel.page_size);

            CreateModel.total_count = Locations.TotalItemCount;
            CreateModel.page_count = Locations.Count;
        }
    }
}