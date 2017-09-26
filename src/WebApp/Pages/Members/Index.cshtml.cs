namespace WebApp.Pages.Members
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
        public IPaginable<Member> Members {get; set;}

        public void OnGet()
        {
            CreateModel = new CreateModel();
            get_members();
        }
       
        public IActionResult OnPostCreateAsync()
        {
            var NumberToCreate = CreateModel.selected_option;

            mediator.Send(new CreateCommand(){NumberToCreate = NumberToCreate});

            CreateModel.message = $"{NumberToCreate} Members Created";            

            get_members();

            return Page();
        }

        public IActionResult OnPostNewPageAsync()
        {
            return Page();
        }

        private void get_members()
        {
            Members =  context.Members.OrderByDescending(t => t.Created).ToPaginable(1, CreateModel.page_size);
            CreateModel.total_count = Members.TotalItemCount;
            CreateModel.page_count = Members.Count;
        }
    }
}