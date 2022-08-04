using LintCoder.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace LintCoder.Identity.Domain.Entities
{
    public partial class SysRole : BaseEntity<string>
    {
        public SysRole()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(36)]
        public string TenantId { get; set; }

        [Required]
        [MaxLength(64)]
        public string RoleName { get; set; }

        [Required]
        [MaxLength(128)]
        public string RoleDesc { get; set; }

        [Required]
        [MaxLength(32)]
        public string RoleCode { get; set; }

        public int? Sort { get; set; }

        public bool? Status { get; set; }
    }
}
