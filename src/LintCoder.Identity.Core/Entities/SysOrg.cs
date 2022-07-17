using LintCoder.Base.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LintCoder.Identity.Domain.Entities
{
    public partial class SysOrg : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new long Id { get; set; }

        [Required]
        public long OrgPid { get; set; }

        [Required]
        [MaxLength(128)]
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
