using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Infrastructure.Authentation.JwtBearer;
using MediatR;
using Microsoft.Extensions.Options;
using Microsoft.Extensions.Primitives;

namespace LintCoder.Identity.API.Application.Commands.Account.RefreshAccessToken
{
    public class RefreshAccessTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, MsgModel>
    {
        private readonly IHttpContextAccessor accessor;
        private readonly IOptions<JwtSettings> options;

        public RefreshAccessTokenCommandHandler(IHttpContextAccessor accessor, IOptions<JwtSettings> options)
        {
            this.accessor = accessor;
            this.options = options;
        }
        public async Task<MsgModel> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var httpContext = accessor.HttpContext;
            //获取请求头部信息token
            httpContext.Request.Headers.TryGetValue("Authorization", out StringValues oldToken);
            var refreshToken = JwtHelpers.CreateRefreshToken(oldToken, options.Value);
            return await Task.FromResult(MsgModel.Success(refreshToken));
        }
    }
}
