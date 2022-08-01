using LintCoder.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LintCoder.Identity.Domain.Entities
{
    public class SysApi : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new long Id { get; set; }

        [Required]
        public long ApiPid { get; set; }

        [Required]
        public string ApiPids { get; set; }

        [Required]
        public bool IsLeaf { get; set; }

        [Required]
        public string ApiName { get; set; }

        [MaxLength(128)]
        public string Url { get; set; }
        public int? Sort { get; set; }

        [Required]
        public int Level { get; set; }

        [Required]
        public bool Status { get; set; }
    }
}
