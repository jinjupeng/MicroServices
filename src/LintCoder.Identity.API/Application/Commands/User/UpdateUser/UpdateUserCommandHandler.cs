using LintCoder.Base;
using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Identity.Domain.Entities;
using Mapster;
using MediatR;

namespace LintCoder.Identity.API.Application.Commands.User.UpdateUser
{
    public class UpdateUserCommandHandler : IRequestHandler<UpdateUserCommand, MsgModel>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<UpdateUserCommandHandler> _logger;
        private readonly IBaseRepository<SysUser> _baseRepository;

        public UpdateUserCommandHandler(IMediator mediator,
            ILogger<UpdateUserCommandHandler> logger,
            IBaseRepository<SysUser> baseRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(IMediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<UpdateUserCommandHandler>));
            _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(IBaseRepository<SysUser>));

        }

        public async Task<MsgModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _baseRepository.GetEntityAsync(x => x.Id == request.Id);
            if(user == null)
            {
                return MsgModel.Fail("更新用户不存在");
            }
            var updateUser = request.UpdateUserRequest.Adapt(user);
            await _baseRepository.UpdateAsync(updateUser, cancellationToken);
            if(await _baseRepository.SaveChangesAsync() > 0)
            {
                return MsgModel.Success("更新成功");
            }
            else
            {
                return MsgModel.Fail("更新失败");
            }
        }
    }
}