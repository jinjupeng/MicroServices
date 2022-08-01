using LintCoder.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LintCoder.Identity.Domain.Entities
{
    public partial class SysMenu : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new long Id { get; set; }

        [Required]
        public long MenuPid { get; set; }

        [Required]
        [MaxLength(64)]
        public string MenuPids { get; set; }

        [Required]
        public bool IsLeaf { get; set; }

        [Required]
        [MaxLength(64)]
        public string MenuName { get; set; }

        [MaxLength(128)]
        public string Url { get; set; }

        [MaxLength(64)]
        public string Icon { get; set; }
        public int? Sort { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
