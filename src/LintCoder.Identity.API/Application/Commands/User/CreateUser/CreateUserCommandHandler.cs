using Common.Utility.Utils;
using Lintcoder.Base;
using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Identity.Domain.Entities;
using LintCoder.Identity.Infrastructure;
using Mapster;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace LintCoder.Identity.API.Application.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, MsgModel>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IBaseRepository<SysUser> _baseRepository;
        private readonly IdentityDbContext dbContext;

        public CreateUserCommandHandler(IMediator mediator,
            ILogger<CreateUserCommandHandler> logger,
            IBaseRepository<SysUser> baseRepository,
            IdentityDbContext dbContext)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(IMediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<CreateUserCommandHandler>));
            _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(IBaseRepository<SysUser>));
            this.dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
        }

        public async Task<MsgModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var sysUser = request.Adapt<SysUser>();
            if(await _baseRepository.IsExistAsync(x => x.UserName == sysUser.UserName))
            {
                return MsgModel.Fail("登录名重复，新增失败！");
            }
            var sysConfig = await dbContext.SysConfig.AsNoTracking().FirstOrDefaultAsync(x => x.ParamKey == "user.init.password");
            sysUser.Password = PasswordEncoder.Encode(sysConfig == null ? "123456" : sysConfig.ParamValue);
            await dbContext.SysUser.AddAsync(sysUser);
            if(await dbContext.SaveChangesAsync() > 0)
            {
                return MsgModel.Success("新增成功");
            }
            else
            {
                return MsgModel.Fail("新增失败");
            }
        }
    }
}
