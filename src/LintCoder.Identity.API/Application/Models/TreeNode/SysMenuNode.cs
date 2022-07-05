using LintCoder.Identity.API.Application.Models.DataTree;
using LintCoder.Identity.Domain.Entities;

namespace LintCoder.Identity.API.Application.Models.TreeNode
{
    public class SysMenuNode : SysMenu, IDataTree<SysMenuNode, long>
    {
        public List<SysMenuNode> Children { get; set; }

        public string path { get => Url; }

        public string name { get => MenuName; }

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
