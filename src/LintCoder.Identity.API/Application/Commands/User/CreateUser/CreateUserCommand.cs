using LintCoder.Identity.API.Infrastructure.ActionResult;
using LintCoder.Identity.Domain.Entities;
using MediatR;
using System.Runtime.Serialization;

namespace LintCoder.Identity.API.Application.Commands.User.CreateUser
{    
    // DDD and CQRS patterns comment: Note that it is recommended to implement immutable Commands
    // In this case, its immutability is achieved by having all the setters as private
    // plus only being able to update the data just once, when creating the object through its constructor.
    // References on Immutable Commands:  
    // http://cqrs.nu/Faq
    // https://docs.spine3.org/motivation/immutability.html 
    // http://blog.gauffin.org/2012/06/griffin-container-introducing-command-support/
    // https://docs.microsoft.com/dotnet/csharp/programming-guide/classes-and-structs/how-to-implement-a-lightweight-class-with-auto-implemented-properties

    [DataContract]
    public class CreateUserCommand : IRequest<MsgModel<SysUser>>
    {
        /// <summary>
        /// 用户名
        /// </summary>
        [DataMember]
        public string UserName { get; set; }

        /// <summary>
        /// 密码
        /// </summary>
        [DataMember]
        public string Password { get; set; }

        /// <summary>
        /// 昵称
        /// </summary>
        [DataMember]
        public string NickName { get; set; }

        /// <summary>
        /// 头像图片路径
        /// </summary>
        [DataMember]
        public string Portrait { get; set; }


        /// <summary>
        /// 组织id
        /// </summary>
        [DataMember]
        public long OrgId { get; set; }

        /// <summary>
        /// 0无效用户，1是有效用户
        /// </summary>
        [DataMember]
        public bool? Enabled { get; set; }

        /// <summary>
        /// 手机号
        /// </summary>
        [DataMember]
        public string Phone { get; set; }

        /// <summary>
        /// email
        /// </summary>
        [DataMember]
        public string Email { get; set; }
    }
}
