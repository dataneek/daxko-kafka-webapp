using System.Collections.Generic;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebApp.Core
{
    public class GetRandomLocationsTask : IGetRandomLocationsTask
    {
        private readonly AppDbContext context;

        private const string sql = @"select top {0} * from Location order by newid()";


        public GetRandomLocationsTask(AppDbContext context)
        {
            this.context = context;
        }

        public List<Location> get(int NumberToGet)
        {
            return context.Locations.FromSql(string.Format(sql, NumberToGet)).AsNoTracking().ToList();
        }
    }

    public interface IGetRandomLocationsTask
    {
        List<Location> get(int NumberToGet);
    }
}