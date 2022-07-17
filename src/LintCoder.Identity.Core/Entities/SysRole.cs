using LintCoder.Base.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LintCoder.Identity.Domain.Entities
{
    public partial class SysRole : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new long Id { get; set; }

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
