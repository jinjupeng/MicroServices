using LintCoder.Base;
using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Identity.Domain.Entities;
using MediatR;

namespace LintCoder.Identity.API.Application.Commands.User.DeleteUser
{
    public class DeleteUserCommandHandler : IRequestHandler<DeleteUserCommand, MsgModel>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<DeleteUserCommandHandler> _logger;
        private readonly IBaseRepository<SysUser> _baseRepository;

        public DeleteUserCommandHandler(IMediator mediator,
            ILogger<DeleteUserCommandHandler> logger,
            IBaseRepository<SysUser> baseRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(IMediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<DeleteUserCommandHandler>));
            _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(IBaseRepository<SysUser>));

        }

        public async Task<MsgModel> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _baseRepository.GetEntityAsync(x => x.Id == request.Id);
            if(user == null)
            {
                return MsgModel.Fail("用户不存在，无法删除！");
            }
            _baseRepository.Remove(user);
            if(await _baseRepository.SaveChangesAsync() > 0)
            {
                return MsgModel.Success("删除成功！");
            }
            else
            {
                return MsgModel.Fail("删除失败！");
            }
        }
    }
}
