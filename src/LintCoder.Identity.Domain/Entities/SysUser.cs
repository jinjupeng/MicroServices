using LintCoder.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace LintCoder.Identity.Domain.Entities
{
    public partial class SysUser : BaseEntity<string>
    {
        public SysUser()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(36)]
        public string TenantId { get; set; }

        [Required]
        [MaxLength(64)]
        public string UserName { get; set; }

        [Required]
        [MaxLength(64)]
        public string Password { get; set; }

        [MaxLength(64)]
        public string NickName { get; set; }

        [MaxLength(255)]
        public string Portrait { get; set; }

        [Required]
        public string OrgId { get; set; }

        [Required]
        public bool Enabled { get; set; }

        [MaxLength(16)]
        public string Phone { get; set; }

        [Required]
        public bool EmailVerified { get; set; }

        [Required]
        public bool IsEmailConfirmed { get; set; }

        [MaxLength(32)]
        public string Email { get; set; }

        [Required]
        public int Sex { get; set; }
    }
}
