using LintCoder.Identity.API.Application.Commands.Account.Login;
using LintCoder.Identity.API.Application.Commands.Account.RefreshAccessToken;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace LintCoder.Identity.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class AccountController : ControllerBase
    {
        private readonly ISender _sender;
        public AccountController(ISender sender)
        {
            _sender = sender;
        }

        [AllowAnonymous]
        [HttpPost("Login/{tenantId}")]
        public async Task<IActionResult> LoginAsync([FromRoute] string tenantId, [FromBody] LoginCommand loginCommand)
        {
            loginCommand.TenantId = tenantId;
            var result = await _sender.Send(loginCommand);
            return Ok(result);
        }

        /// <summary>
        /// 刷新token
        /// </summary>
        /// <returns></returns>
        //[AllowAnonymous]
        [HttpPut("RefreshAccessToken")]
        public async Task<IActionResult> RefreshAccessTokenAsync()
        {
            var result = await _sender.Send(new RefreshAccessTokenCommand());
            return Ok(result);
        }
    }
}
