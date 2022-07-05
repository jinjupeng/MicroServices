using System.Collections.Generic;

namespace LintCoder.Identity.API.Application.Models.TreeNode
{
    public class RoleCheckedIds
    {
        public long RoleId { get; set; }
        public List<long> CheckedIds { get; set; }

    }
}
