using LintCoder.Identity.API.Application.Commands.User.CreateUser;
using LintCoder.Identity.API.Application.Commands.User.DeleteUser;
using LintCoder.Identity.API.Application.Commands.User.UpdateUser;
using LintCoder.Identity.API.Application.Queries.User.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LintCoder.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SysUserController : ControllerBase
    {
        private readonly ISender _sender;
        public SysUserController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// 用户列表查询接口
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetPagedAsync([FromQuery] QueryUsersCommand query)
        {
            var result = await _sender.Send(query);
            return Ok(result);
        }

        /// <summary>
        /// 新增用户
        /// </summary>
        /// <param name="createUserCommand"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserCommand createUserCommand)
        {
            var result = await _sender.Send(createUserCommand);
            return Ok(result);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="id">id</param>
        /// <param name="updateUserCommand">用户信息</param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync([FromRoute] long id, [FromBody] UpdateUserCommand updateUserCommand)
        {
            var result = await _sender.Send(updateUserCommand);
            return Ok(result);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] long id)
        {
            var result = await _sender.Send(new DeleteUserCommand { Id = id });
            return Ok(result);
        }
    }
}
