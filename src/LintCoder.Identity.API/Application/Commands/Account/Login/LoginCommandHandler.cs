using Common.Utility.Utils;
using LintCoder.Application.Common.Models;
using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Identity.Infrastructure;
using LintCoder.Infrastructure.Authentation.JwtBearer;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

namespace LintCoder.Identity.API.Application.Commands.Account.Login
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, MsgModel>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<LoginCommandHandler> _logger;
        private readonly IdentityDbContext dbContext;
        private readonly IOptions<JwtSettings> options;

        public LoginCommandHandler(IMediator mediator,
            ILogger<LoginCommandHandler> logger,
            IdentityDbContext dbContext,
            IOptions<JwtSettings> options)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(IMediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<LoginCommandHandler>));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
            this.options = options;
        }

        public async Task<MsgModel> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            if(string.IsNullOrWhiteSpace(request.TenantId) || string.IsNullOrWhiteSpace(request.UserName) || string.IsNullOrWhiteSpace(request.Password))
            {
                return MsgModel.Fail("参数为空！");
            }
            var tenantInfo = await dbContext.TenantInfo.AsNoTracking().FirstOrDefaultAsync(x => x.Id == request.TenantId);
            if(tenantInfo == null)
            {
                return MsgModel.Fail("租户信息不存在！");
            }
            if(tenantInfo.IsActive == false)
            {
                return MsgModel.Fail("租户未激活，请联系管理员！");
            }
            // 加密登陆密码
            string encodePassword = PasswordEncoder.Encode(request.Password);
            var loginUser = await dbContext.SysUser.AsNoTracking().FirstOrDefaultAsync(x => x.TenantId == request.TenantId && x.UserName == request.UserName && x.Password == encodePassword);
            if(loginUser == null)
            {
                return MsgModel.Fail("用户名或密码不正确！");
            }
            if(loginUser.Enabled == false)
            {
                return MsgModel.Fail("账户已被禁用！");
            }
            var userRoleList = await dbContext.SysUserRole.AsNoTracking().Where(x => x.UserId == loginUser.Id).Select(x => x.RoleId).ToListAsync();
            var roleNameList = await dbContext.SysRole.AsNoTracking().Where(x => userRoleList.Contains(x.Id)).Select(x => x.RoleCode).ToListAsync();
            var tokenRequest = new TokenRequest
            {
                UserId = loginUser.Id.ToString(),
                UserName = loginUser.UserName,
                TennantId = loginUser.TenantId,
                Email = loginUser.Email,
                IsEmailConfirmed = loginUser.IsEmailConfirmed,
                PhoneNumber = loginUser.Phone,
                PhoneNumberVerified = loginUser.IsEmailConfirmed,
                Roles = roleNameList
            };
            var token = JwtHelpers.CreateAccessToken(tokenRequest, options.Value);
            return MsgModel.Success(token);
        }
    }
}
