using Lintcoder.Base.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Lintcoder.Base
{
    public class BaseDbContext : DbContext
    {
        private readonly IHttpContextAccessor _httpContextAccessor;
        public BaseDbContext(DbContextOptions options, IHttpContextAccessor httpContextAccessor) : base(options)
        {
            if (httpContextAccessor != null)
            {
                _httpContextAccessor = httpContextAccessor;
            }
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
            var user = _httpContextAccessor?.HttpContext?.User?.Identity?.Name ?? "";
            foreach (var entry in entries)
            {
                switch (entry.State)
                {
                    case EntityState.Modified:
                        entry.Entity.ModifiedTime = dateTime;
                        break;
                    case EntityState.Added:
                        entry.Entity.CreatedTime = dateTime;
                        break;
                }
            }
        }
    }
}

