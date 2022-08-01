using Finbuckle.MultiTenant;
using LintCoder.Application.Common.Exceptions;
using LintCoder.Application.Multitenancy;
using Mapster;

namespace LintCoder.Infrastructure.MultiTenancy
{
    internal class TenantService : ITenantService
    {
        private readonly IMultiTenantStore<TenantEntity> _tenantStore;

        public TenantService(IMultiTenantStore<TenantEntity> tenantStore)
        {
            _tenantStore = tenantStore;
        }

        public async Task<List<TenantDto>> GetAllAsync()
        {
            var tenants = (await _tenantStore.GetAllAsync()).Adapt<List<TenantDto>>();
            return tenants;
        }

        public async Task<bool> ExistsWithIdAsync(string id) =>
            await _tenantStore.TryGetAsync(id) is not null;

        public async Task<bool> ExistsWithNameAsync(string name) =>
            (await _tenantStore.GetAllAsync()).Any(t => t.Name == name);

        public async Task<TenantDto> GetByIdAsync(string id) =>
            (await GetTenantInfoAsync(id))
                .Adapt<TenantDto>();

        public async Task<string> CreateAsync(CreateTenantRequest request, CancellationToken cancellationToken)
        {
            var tenant = new TenantEntity(request.Id, request.Name, request.ConnectionString, request.AdminEmail, request.Issuer);
            await _tenantStore.TryAddAsync(tenant);

            return tenant.Id;
        }

        public async Task<string> ActivateAsync(string id)
        {
            var tenant = await GetTenantInfoAsync(id);

            if (tenant.IsActive)
            {
                throw new ConflictException("Tenant is already Activated.");
            }

            tenant.Activate();

            await _tenantStore.TryUpdateAsync(tenant);

            return $"Tenant {id} is now Activated.";
        }

        public async Task<string> DeactivateAsync(string id)
        {
            var tenant = await GetTenantInfoAsync(id);

            if (!tenant.IsActive)
            {
                throw new ConflictException("Tenant is already Deactivated.");
            }

            tenant.Deactivate();

            await _tenantStore.TryUpdateAsync(tenant);

            return $"Tenant {id} is now Deactivated.";
        }

        public async Task<string> UpdateSubscription(string id, DateTime extendedExpiryDate)
        {
            var tenant = await GetTenantInfoAsync(id);

            tenant.SetValidity(extendedExpiryDate);

            await _tenantStore.TryUpdateAsync(tenant);

            return $"Tenant {id}'s Subscription Upgraded. Now Valid till {tenant.ValidUpto}.";
        }

        private async Task<TenantEntity> GetTenantInfoAsync(string id) =>
            await _tenantStore.TryGetAsync(id)
                ?? throw new NotFoundException($"{typeof(TenantEntity).Name} {id} Not Found.");
    }
}
