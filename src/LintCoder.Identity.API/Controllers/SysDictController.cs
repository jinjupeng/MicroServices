using LintCoder.Identity.API.Application.Queries.Dict.QueryDict;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace LintCoder.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SysDictController : ControllerBase
    {
        private readonly ISender _sender;
        public SysDictController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// 字典查询
        /// </summary>
        /// <param name="query"></param>
        /// <returns></returns>
        [HttpGet("GetPaged")]
        public async Task<IActionResult> GetPagedAsync([FromQuery] QueryDictCommand query)
        {
            var result = await _sender.Send(query);
            return Ok(result);
        }
    }
}
