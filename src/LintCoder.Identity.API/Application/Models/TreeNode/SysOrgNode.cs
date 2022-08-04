using LintCoder.Identity.API.Application.Models.DataTree;
using LintCoder.Identity.Domain.Entities;

namespace LintCoder.Identity.API.Application.Models.TreeNode
{
    public class SysOrgNode : SysOrg, IDataTree<SysOrgNode, string>
    {
        public List<SysOrgNode> Children { get; set; }
        public string GetId()
        {
            return Id;
        }

        public string GetParentId()
        {
            return OrgPid;
        }


    }
}
