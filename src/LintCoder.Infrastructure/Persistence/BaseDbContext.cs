using LintCoder.Application.Common.Interfaces;
using LintCoder.Domain.Common;
using Microsoft.EntityFrameworkCore;

namespace LintCoder.Infrastructure.Persistence
{
    public class BaseDbContext : DbContext
    {
        private readonly ICurrentUser currentUser;

        public BaseDbContext(DbContextOptions options, ICurrentUser currentUser) : base(options)
        {
            this.currentUser = currentUser;
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
                        entry.Entity.ModifiedBy = Convert.ToString(currentUser.GetUserId());
                        entry.Entity.ModifiedName = currentUser.UserName;
                        entry.Entity.ModifiedTime = dateTime;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedBy = currentUser.GetUserId().ToString();
                        entry.Entity.CreatedName = currentUser.UserName;
                        entry.Entity.CreatedTime = dateTime;
                        break;
                }
            }
        }
    }
}

