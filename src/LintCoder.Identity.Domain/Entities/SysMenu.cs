using LintCoder.Domain.Common;
using System.ComponentModel.DataAnnotations;

namespace LintCoder.Identity.Domain.Entities
{
    public partial class SysMenu : BaseEntity<string>
    {
        public SysMenu()
        {
            Id = Guid.NewGuid().ToString();
        }

        [Required]
        [MaxLength(36)]
        public string TenantId { get; set; }

        [Required]
        public string MenuPid { get; set; }

        [Required]
        [MaxLength(256)]
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
