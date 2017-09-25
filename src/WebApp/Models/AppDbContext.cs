namespace WebApp.Models
{
    using System.Data;
    using System.Threading.Tasks;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.EntityFrameworkCore.Storage;
    
    public class AppDbContext : DbContext
    {
        private IDbContextTransaction currentTransaction;

        public AppDbContext(DbContextOptions options)
            : base(options) { }


        public DbSet<Member> Members { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<LocationCheckin> LocationCheckin { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Member>()
                .ToTable("Member", "dbo")
                .HasKey(t => t.MemberId);

            modelBuilder.Entity<Member>()
                .Property(t => t.Watermark).IsRowVersion();


            modelBuilder.Entity<Location>()
                .ToTable("Location", "dbo")
                .HasKey(t => t.LocationId);

            modelBuilder.Entity<Location>()
                .Property(t => t.Watermark).IsRowVersion();


            modelBuilder.Entity<LocationCheckin>()
                .ToTable("LocationCheckin", "dbo")
                .HasKey(t => t.LocationCheckinId);

            modelBuilder.Entity<LocationCheckin>()
                .Property(t => t.Watermark).IsRowVersion();

            modelBuilder.Entity<LocationCheckin>()
                .HasOne(t => t.Member).WithMany().HasForeignKey(t => t.MemberId);

            modelBuilder.Entity<LocationCheckin>()
                .HasOne(t => t.Location).WithMany().HasForeignKey(t => t.LocationId);
        }

        public async Task BeginTransactionAsync()
        {
            if (currentTransaction != null)
            {
                return;
            }

            currentTransaction = await Database.BeginTransactionAsync(IsolationLevel.ReadCommitted);
        }

        public async Task CommitTransactionAsync()
        {
            try
            {
                await SaveChangesAsync();

                currentTransaction?.Commit();
            }
            catch
            {
                RollbackTransaction();
                throw;
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }

        public void RollbackTransaction()
        {
            try
            {
                currentTransaction?.Rollback();
            }
            finally
            {
                if (currentTransaction != null)
                {
                    currentTransaction.Dispose();
                    currentTransaction = null;
                }
            }
        }
    }
}