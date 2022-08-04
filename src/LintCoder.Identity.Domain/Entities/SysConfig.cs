using LintCoder.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace LintCoder.Identity.Domain.Entities
{
    public partial class SysConfig : BaseEntity<string>
    {
        public SysConfig()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(36)]
        public string TenantId { get; set; }

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
