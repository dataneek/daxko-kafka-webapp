namespace WebApp.Pages.Members
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Hangfire;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;

    public class IndexModel : PageModel
    {
        public string Message { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPostAsync()
        {
            BackgroundJob.Enqueue(() => Console.WriteLine("this is a member job"));

            return Page();
        }
    }
}