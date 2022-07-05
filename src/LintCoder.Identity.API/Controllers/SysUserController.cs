using LintCoder.Identity.API.Application.Queries.User.GetUsers;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace LintCoder.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
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
        [HttpPost]
        [Route("query")]
        public async Task<IActionResult> Query([FromBody] GetUsersQuery query)
        {
            var result = await _sender.Send(query);
            return Ok(result);
        }
    }
}
