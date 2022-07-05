namespace LintCoder.Identity.API.Application.Models.DataTree
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    /// <typeparam name="ID"></typeparam>
    public interface IDataTree<T, ID> where T : class
    {
        /// <summary>
        /// 维护树形关系：元素一
        /// </summary>
        /// <returns></returns>
        public ID GetId();

        /// <summary>
        /// 维护树形关系：元素二
        /// </summary>
        /// <returns></returns>
        public ID GetParentId();

        /// <summary>
        /// 子节点数组
        /// </summary>
        public List<T> Children { get; set; }

    }
}
