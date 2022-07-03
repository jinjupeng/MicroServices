using Lintcoder.Base.Entities;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LintCoder.Identity.Domain.Entities
{
    public partial class SysUserRole : BaseEntity
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public new long Id { get; set; }

        [Required]
        public long RoleId { get; set; }

        [Required]
        public long UserId { get; set; }
    }
}
