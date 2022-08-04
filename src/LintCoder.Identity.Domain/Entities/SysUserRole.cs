using LintCoder.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace LintCoder.Identity.Domain.Entities
{
    public partial class SysUserRole : BaseEntity<string>
    {
        public SysUserRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(36)]
        public string TenantId { get; set; }

        [Required]
        public string RoleId { get; set; }

        [Required]
        public string UserId { get; set; }
    }
}
