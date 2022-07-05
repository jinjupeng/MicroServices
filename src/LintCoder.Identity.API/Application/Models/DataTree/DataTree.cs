namespace LintCoder.Identity.API.Application.Models.DataTree
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="ID"></typeparam>
    public class DataTree<T, ID> where T : class, IDataTree<T, ID>
    {
        /// <summary>
        /// 构造无根树形结构数据，比如系统左侧菜单栏
        /// </summary>
        /// <param name="paramList"></param>
        /// <param name="rootNodeId"></param>
        /// <returns></returns>
        public static List<T> BuildTreeWithoutRoot(List<T> paramList, ID rootNodeId)
        {
            List<T> returnList = new List<T>();
            //查找根节点
            foreach (T node in paramList)
            {
                //从2级节点开始构造
                if (Convert.ToString(node.GetParentId()) == Convert.ToString(rootNodeId))
                {
                    returnList.Add(node);
                }
            }
            foreach (T entry in paramList)
            {
                ToTreeChildren(returnList, entry);
            }
            return returnList;
        }

        /// <summary>
        /// 构造只有一个根的树形结构数据
        /// </summary>
        /// <param name="paramList"></param>
        /// <param name="rootNodeId"></param>
        /// <returns></returns>
        public static List<T> BuildTree(List<T> paramList, ID rootNodeId)
        {
            List<T> returnList = new List<T>();
            // 查找根节点
            foreach (T node in paramList)
            {
                //从1级节点开始构造
                if (Convert.ToString(node.GetId()) == Convert.ToString(rootNodeId))
                {
                    returnList.Add(node);
                }
            }
            foreach (T entry in paramList)
            {
                ToTreeChildren(returnList, entry);
            }
            return returnList;
        }

        private static void ToTreeChildren(List<T> returnList, T entry)
        {
            foreach (T node in returnList)
            {
                if (Convert.ToString(entry.GetParentId()) == Convert.ToString(node.GetId()))
                {
                    if (node.Children == null)
                    {
                        node.Children = new List<T>();
                    }
                    node.Children.Add(entry);
                }
                if (node.Children != null)
                {
                    ToTreeChildren(node.Children, entry);
                }
            }
        }
    }
}
