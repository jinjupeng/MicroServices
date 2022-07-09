using Common.Utility.Utils;
using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Identity.Infrastructure;
using LintCoder.Shared.Authentication.JwtBearer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace LintCoder.Identity.API.Application.Commands.Account.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, MsgModel>
    {

        private readonly IMediator _mediator;
        private readonly ILogger<LoginCommandHandler> _logger;
        private readonly IdentityDbContext dbContext;
        private readonly JwtHelper jwtHelper;

        public LoginCommandHandler(IMediator mediator,
            ILogger<LoginCommandHandler> logger,
            IdentityDbContext dbContext,
            JwtHelper jwtHelper)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(IMediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<LoginCommandHandler>));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.jwtHelper = jwtHelper ?? throw new ArgumentNullException(nameof(jwtHelper));
        }

        public async Task<MsgModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {

            // 加密登陆密码
            string encodePassword = PasswordEncoder.Encode(request.Password);
            var loginUser = await dbContext.SysUser.AsNoTracking().FirstOrDefaultAsync(x => x.UserName == request.UserName && x.Password == encodePassword);
            if(loginUser == null)
            {
                return MsgModel.Fail("用户名或密码不正确！");
            }
            if(loginUser.Enabled == false)
            {
                return MsgModel.Fail("账户已被禁用！");
            }
            var userRoleIds = await dbContext.SysUserRole.AsNoTracking().Where(x => x.UserId == loginUser.Id).Select(x => new { x.RoleId }).ToListAsync();
            var token = jwtHelper.CreateAccessToken(loginUser.UserName, loginUser.Id.ToString(), loginUser.UserName, JsonSerializer.Serialize(userRoleIds));
            return MsgModel.Success(token);
        }
    }
}
