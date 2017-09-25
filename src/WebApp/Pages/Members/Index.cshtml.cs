namespace WebApp.Pages.Members
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Hangfire;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.RazorPages;
    using MediatR;

    public class IndexModel : PageModel
    {
        private readonly IMediator mediator;

        public IndexModel(IMediator mediator)
        {
            this.mediator = mediator;
        }

        public string Message { get; set; }

        public void OnGet()
        {

        }

        public IActionResult OnPostAsync()
        {
            this.mediator.Send(new CreateCommand { NumberToCreate = 5 });
            //BackgroundJob.Enqueue(() => Console.WriteLine("this is a member job"));

            return Page();
        }
    }
}