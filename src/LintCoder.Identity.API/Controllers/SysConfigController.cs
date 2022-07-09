using LintCoder.Identity.API.Application.Queries.Config.QueryConfig;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LintCoder.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SysConfigController : ControllerBase
    {
        private readonly ISender _sender;
        public SysConfigController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// 配置查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("GetPaged")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] QueryConfigCommand query)
        {
            var result = await _sender.Send(query);
            return Ok(result);
        }
    }
}
