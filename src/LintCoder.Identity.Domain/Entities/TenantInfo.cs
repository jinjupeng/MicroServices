using LintCoder.Domain.Common;

namespace LintCoder.Identity.Domain.Entities
{
    public class TenantInfo : BaseEntity<string>
    {
        public TenantInfo()
        {
            Id = Guid.NewGuid().ToString();
        }

        public string TennantName { get;set;}

        public bool IsActive { get; set; }
    }
}
