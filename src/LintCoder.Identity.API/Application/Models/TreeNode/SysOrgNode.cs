using LintCoder.Identity.API.Application.Models.DataTree;
using LintCoder.Identity.Domain.Entities;

namespace LintCoder.Identity.API.Application.Models.TreeNode
{
    public class SysOrgNode : SysOrg, IDataTree<SysOrgNode, long>
    {
        public List<SysOrgNode> Children { get; set; }
        public long GetId()
        {
            return Id;
        }

        public long GetParentId()
        {
            return OrgPid;
        }


    }
}
