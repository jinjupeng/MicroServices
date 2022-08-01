using LintCoder.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LintCoder.Identity.Domain.Entities
{
    public partial class SysUser : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new long Id { get; set; }

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
        public long OrgId { get; set; }

        [Required]
        public bool Enabled { get; set; }

        [MaxLength(16)]
        public string Phone { get; set; }

        [MaxLength(32)]
        public string Email { get; set; }

        [Required]
        public int Sex { get; set; }
    }
}
