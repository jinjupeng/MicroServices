using LintCoder.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace LintCoder.Identity.Domain.Entities
{
    public class SysApi : BaseEntity<string>
    {
        public SysApi()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(36)]
        public string TenantId { get; set; }

        [Required]
        public string ApiPid { get; set; }

        [Required]
        [MaxLength(256)]
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
