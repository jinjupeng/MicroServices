using LintCoder.Domain.Common;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LintCoder.Identity.Domain.Entities
{
    public partial class SysConfig : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new long Id { get; set; }

        [Required]
        [MaxLength(64)]
        public string ParamName { get; set; }

        [Required]
        [MaxLength(64)]
        public string ParamKey { get; set; }

        [Required]
        [MaxLength(64)]
        public string ParamValue { get; set; }

        [MaxLength(128)]
        public string ParamDesc { get; set; }
    }
}
