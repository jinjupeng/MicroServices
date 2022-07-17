using LintCoder.Base.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LintCoder.Identity.Domain.Entities
{
    public partial class SysDict : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new long Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string GroupName { get; set; }

        [Required]
        [MaxLength(64)]
        public string GroupCode { get; set; }

        [Required]
        [MaxLength(64)]
        public string ItemName { get; set; }

        [Required]
        [MaxLength(64)]
        public string ItemValue { get; set; }

        [MaxLength(128)]
        public string ItemDesc { get; set; }
    }
}
