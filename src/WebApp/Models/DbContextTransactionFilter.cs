namespace WebApp.Models
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class DbContextTransactionFilter : IAsyncActionFilter
    {
        private readonly AppDbContext context;

        public DbContextTransactionFilter(AppDbContext context)
        {
            this.context = context;
        }

        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            try
            {
                await this.context.BeginTransactionAsync();
                await next();
                await this.context.CommitTransactionAsync();
            }
            catch (Exception)
            {
                this.context.RollbackTransaction();
                throw;
            }
        }
    }
}