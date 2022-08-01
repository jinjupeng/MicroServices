

namespace LintCoder.Domain.Common
{
    /// <summary>
    /// 实体
    /// </summary>
    public interface IEntity
    {/// <summary>
     /// 创建时间
     /// </summary>
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        public string CreatedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifiedTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        public string CreatedName { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        public string? ModifiedName { get; set; }

    }
}
