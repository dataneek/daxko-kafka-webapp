namespace WebApp.Pages
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using WebApp.Models;

    public class IndexModel : PageModel
    {
        private readonly AppDbContext context;

        public IndexModel(AppDbContext context)
        {
            this.context = context;
        }

        public HomePageModel HomePageModel { get; set; }

        public void OnGet()
        {
            HomePageModel = new HomePageModel(){
                total_members = context.Members.Count(),
                total_locations = context.Locations.Count(),
                total_location_checkins = context.LocationCheckin.Count()
            };
        }
    }
}