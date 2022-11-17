using LintCoder.Identity.API.Application.Models.DataTree;
using LintCoder.Identity.Domain.Entities;

namespace LintCoder.Identity.API.Application.Models.TreeNode
{
    public class SysApiNode : SysApi, IDataTree<SysApiNode, string>
    {
        public List<SysApiNode> Children { get; set; }
        public string GetParentId()
        {
            return ApiPid;
        }

        public string GetId()
        {
            return Id;
        }

    }
}
