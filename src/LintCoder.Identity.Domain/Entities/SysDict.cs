using LintCoder.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace LintCoder.Identity.Domain.Entities
{
    public partial class SysDict : BaseEntity<string>
    {
        public SysDict()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(36)]
        public string TenantId { get; set; }

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
