using LintCoder.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace LintCoder.Identity.Domain.Entities
{
    public partial class SysOrg : BaseEntity<string>
    {
        public SysOrg()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(36)]
        public string TenantId { get; set; }

        [Required]
        public string OrgPid { get; set; }

        [Required]
        [MaxLength(256)]
        public string OrgPids { get; set; }

        [Required]
        public bool IsLeaf { get; set; }

        [Required]
        [MaxLength(64)]
        public string OrgName { get; set; }

        [MaxLength(128)]
        public string Address { get; set; }

        [MaxLength(16)]
        public string Phone { get; set; }

        [MaxLength(32)]
        public string Email { get; set; }

        public int? Sort { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
