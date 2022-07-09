using LintCoder.Identity.API.Application.Queries.Menu.QueryMenuTree;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LintCoder.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class SysMenuController : ControllerBase
    {

        private readonly ISender _sender;
        public SysMenuController(ISender sender)
        {
            _sender = sender;
        }

        /// <summary>
        /// 获取侧边栏路由菜单
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetMenusForRouter")]
        public async Task<IActionResult> GetMenusForRouterAsync()
        {
            var result = await _sender.Send(new QueryMenuTreeCommand());
            return Ok(result);
        }
    }
}
