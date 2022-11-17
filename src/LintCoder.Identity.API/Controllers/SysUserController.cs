using LintCoder.Identity.API.Application.Commands.User.ChangePassword;
using LintCoder.Identity.API.Application.Commands.User.CreateUser;
using LintCoder.Identity.API.Application.Commands.User.DeleteUser;
using LintCoder.Identity.API.Application.Commands.User.UpdateUser;
using LintCoder.Identity.API.Application.Models.Request;
using LintCoder.Identity.API.Application.Queries.User.GetUsers;
using LintCoder.Identity.API.Application.Queries.User.QueryCurrentUser;
using LintCoder.Identity.API.Application.Queries.User.QueryPasswordIsDefault;
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
        [HttpGet("GetPaged")]
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
        [HttpPost("Create")]
        public async Task<IActionResult> CreateAsync([FromBody] CreateUserCommand createUserCommand)
        {
            var result = await _sender.Send(createUserCommand);
            return Ok(result);
        }

        /// <summary>
        /// 修改用户
        /// </summary>
        /// <param name="id"></param>
        /// <param name="updateUserRequest">用户信息</param>
        /// <returns></returns>
        [HttpPut("{id}/Update")]
        public async Task<IActionResult> UpdateAsync([FromRoute] string id, [FromBody] UpdateUserRequest updateUserRequest)
        {
            var result = await _sender.Send(new UpdateUserCommand { Id = id, UpdateUserRequest = updateUserRequest });
            return Ok(result);
        }

        /// <summary>
        /// 删除用户
        /// </summary>
        /// <param name="id">用户id</param>
        /// <returns></returns>
        [HttpDelete("{id}/Delete")]
        public async Task<IActionResult> DeleteAsync([FromRoute] string id)
        {
            var result = await _sender.Send(new DeleteUserCommand { Id = id });
            return Ok(result);
        }

        /// <summary>
        /// 获取登录用户个人信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("current")]
        public async Task<IActionResult> GetCurrentUserInfoAsync()
        {
            var result = await _sender.Send(new QueryCurrentUserCommand());
            return Ok(result);
        }

        /// <summary>
        /// 判断当前用户密码是否是初始密码
        /// </summary>
        /// <returns></returns>
        [HttpGet("pwd/IsDefault")]
        public async Task<IActionResult> PasswordIsDefaultAsync()
        {
            var result = await _sender.Send(new QueryPasswordIsDefaultCommand());
            return Ok(result);
        }

        /// <summary>
        /// 重置密码
        /// </summary>
        /// <returns></returns>
        [HttpPut("pwd/change")]
        public async Task<IActionResult> PasswordChangeAsync([FromBody] ChangePasswordCommand request)
        {
            var result = await _sender.Send(request);
            return Ok(result);
        }
    }
}
