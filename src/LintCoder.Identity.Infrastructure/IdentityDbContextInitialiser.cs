using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using LintCoder.Identity.Domain.Entities;

namespace LintCoder.Identity.Infrastructure
{
    public class IdentityDbContextInitialiser
    {
        private readonly IdentityDbContext _context;
        private readonly ILogger<IdentityDbContextInitialiser> _logger;

        public IdentityDbContextInitialiser(IdentityDbContext context, ILogger<IdentityDbContextInitialiser> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task InitializeAsync()
        {
            try
            {
                if (_context.Database.IsSqlServer())
                {
                    await _context.Database.MigrateAsync();
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while initialising the database.");
                throw;
            }
        }

        public async Task SeedAsync()
        {
            try
            {
                await TrySeedAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "An error occurred while seeding the database.");
                throw;
            }
        }

        public async Task TrySeedAsync()
        {
            // Default data
            // Seed, if necessary
            if (!_context.SysUser.Any())
            {
                _context.SysUser.Add(new SysUser
                {
                    UserName = "admin",
                    Enabled = true,
                    NickName = "admin",
                    Password = "123456",
                    Email = "test@test.com"
                });

            }
            // SysRole,Role,.....

            await _context.SaveChangesAsync();
        }
    }
}
