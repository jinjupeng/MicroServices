using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Infrastructure.Auth.JwtBearer;
using MediatR;
using Microsoft.Extensions.Primitives;

namespace LintCoder.Identity.API.Application.Commands.Account.RefreshAccessToken
{
    public class RefreshAccessTokenCommandHandler : IRequestHandler<RefreshAccessTokenCommand, MsgModel>
    {
        private readonly JwtHelper jwtHelper;
        private readonly IHttpContextAccessor accessor;

        public RefreshAccessTokenCommandHandler(JwtHelper jwtHelper, IHttpContextAccessor accessor)
        {
            this.jwtHelper = jwtHelper;
            this.accessor = accessor;
        }
        public async Task<MsgModel> Handle(RefreshAccessTokenCommand request, CancellationToken cancellationToken)
        {
            var httpContext = accessor.HttpContext;
            //获取请求头部信息token
            httpContext.Request.Headers.TryGetValue("Authorization", out StringValues oldToken);
            var refreshToken = jwtHelper.CreateRefreshToken(oldToken);
            return await Task.FromResult(MsgModel.Success(refreshToken));
        }
    }
}
