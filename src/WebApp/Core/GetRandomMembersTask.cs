using System.Collections.Generic;
using WebApp.Models;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WebApp.Core
{
    public class GetRandomMembersTask : IGetRandomMembersTask
    {
        private readonly AppDbContext context;

        private const string sql = @"select top {0} * from Member order by newid()";


        public GetRandomMembersTask(AppDbContext context)
        {
            this.context = context;
        }

        public List<Member> get(int NumberToGet)
        {
            return context.Members.FromSql(string.Format(sql, NumberToGet)).AsNoTracking().ToList();
        }
    }

    public interface IGetRandomMembersTask
    {
        List<Member> get(int NumberToGet);
    }
}