using LintCoder.Identity.API.Application.Models.DataTree;
using LintCoder.Identity.Domain.Entities;

namespace LintCoder.Identity.API.Application.Models.TreeNode
{
    public class SysApiNode : SysApi, IDataTree<SysApiNode, long>
    {
        public List<SysApiNode> Children { get; set; }
        public long GetParentId() { 
            return ApiPid; 
        }

        public long GetId()
        {
            return Id;
        }

    }
}
