namespace WebApp.Pages.Locations
{
    using System.Linq;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using PaginableCollections;
    using WebApp.Models;

    public class IndexModel : PageModel
    {
        const int ItemCountPerPage = 100;
        private readonly AppDbContext context;

        public IndexModel(AppDbContext context)
        {
            this.context = context;
        }

        public IPaginable<Location> Locations { get; private set; }


        public void OnGet(int? pageNumber = 1)
        {
            Locations =
                context.Locations
                    .OrderByDescending(t => t.Created)
                    .ToPaginable(pageNumber.Value, ItemCountPerPage);
        }

        public IActionResult OnPostAsync()
        {
            return Page();
        }
    }
}