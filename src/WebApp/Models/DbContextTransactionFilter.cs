namespace WebApp.Models
{
    using System;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc.Filters;

    public class DbContextTransactionFilter : IAsyncPageFilter
    {
        async Task IAsyncPageFilter.OnPageHandlerExecutionAsync(PageHandlerExecutingContext context, PageHandlerExecutionDelegate next)
        {
            var db = context.HttpContext.RequestServices.GetService(typeof(AppDbContext)) as AppDbContext;
            try
            {
                await db.BeginTransactionAsync();
                await next();
                await db.CommitTransactionAsync();
            }
            catch (Exception)
            {
                db.RollbackTransaction();
                throw;
            }
        }

        Task IAsyncPageFilter.OnPageHandlerSelectionAsync(PageHandlerSelectedContext context)
        {
            return Task.CompletedTask;
        }
    }
}