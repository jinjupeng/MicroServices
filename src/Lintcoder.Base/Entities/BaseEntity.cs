using System.ComponentModel.DataAnnotations;

namespace Lintcoder.Base.Entities
{
    public class BaseEntity<TKey> : IEntity
    {
        [Required]
        public TKey Id { get; set; }

        /// <summary>
        /// 创建时间
        /// </summary>
        [Required]
        public DateTime CreatedTime { get; set; }

        /// <summary>
        /// 创建人
        /// </summary>
        [Required]
        [MaxLength(64)]
        public string CreatedBy { get; set; }

        /// <summary>
        /// 修改时间
        /// </summary>
        public DateTime? ModifiedTime { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [MaxLength(64)]
        public string? ModifiedBy { get; set; }

        /// <summary>
        /// 创建人名称
        /// </summary>
        [MaxLength(16)]
        public string CreatedName { get; set; }

        /// <summary>
        /// 修改人
        /// </summary>
        [MaxLength(16)]
        public string? ModifiedName { get; set; }
    }

    public class BaseEntity : BaseEntity<Guid>
    {

    }
}
