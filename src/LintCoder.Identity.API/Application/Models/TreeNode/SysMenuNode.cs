using LintCoder.Identity.API.Application.Models.DataTree;
using LintCoder.Identity.Domain.Entities;

namespace LintCoder.Identity.API.Application.Models.TreeNode
{
    public class SysMenuNode : IDataTree<SysMenuNode, long>
    {
        public List<SysMenuNode> Children { get; set; }

        public long Id { get; set; }

        public long MenuPid { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public long GetId()
        {
            return Id;
        }

        public long GetParentId()
        {
            return MenuPid;
        }
    }
}
