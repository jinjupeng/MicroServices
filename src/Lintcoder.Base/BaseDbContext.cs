using LintCoder.Application.Users;
using LintCoder.Base.Entities;
using Microsoft.EntityFrameworkCore;

namespace LintCoder.Base
{
    public class BaseDbContext : DbContext
    {
        private readonly UserContext userContext;

        public BaseDbContext(DbContextOptions options, UserContext userContext) : base(options)
        {
            this.userContext = userContext;
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }

        public override int SaveChanges()
        {
            SetTrackInfo();
            return base.SaveChanges();
        }

        public override Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            SetTrackInfo();
            return base.SaveChangesAsync(cancellationToken);
        }

        private void SetTrackInfo()
        {
            ChangeTracker.DetectChanges();

            var entries = this.ChangeTracker.Entries<IEntity>()
                .Where(x => x.State == EntityState.Added || x.State == EntityState.Modified);

            var dateTime = DateTime.Now;
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.ModifiedBy = Convert.ToString(userContext.Id);
                        entry.Entity.ModifiedName = userContext.Name;
                        entry.Entity.ModifiedTime = dateTime;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedBy = Convert.ToString(userContext.Id);
                        entry.Entity.CreatedName = userContext.Name;
                        entry.Entity.CreatedTime = dateTime;
                        break;
                }
            }
        }
    }
}

