using LintCoder.Identity.API.Application.Models.DataTree;

namespace LintCoder.Identity.API.Application.Models.TreeNode
{
    public class SysMenuNode : IDataTree<SysMenuNode, string>
    {
        public List<SysMenuNode> Children { get; set; }

        public string Id { get; set; }

        public string MenuPid { get; set; }

        public string Path { get; set; }

        public string Name { get; set; }

        public string Icon { get; set; }

        public string GetId()
        {
            return Id;
        }

        public string GetParentId()
        {
            return MenuPid;
        }
    }
}
