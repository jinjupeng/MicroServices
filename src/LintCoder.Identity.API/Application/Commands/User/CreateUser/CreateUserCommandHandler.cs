using Lintcoder.Base;
using LintCoder.Identity.API.Application.Models.Response;
using LintCoder.Identity.Domain.Entities;
using MediatR;

namespace LintCoder.Identity.API.Application.Commands.User.CreateUser
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, MsgModel>
    {
        private readonly IMediator _mediator;
        private readonly ILogger<CreateUserCommandHandler> _logger;
        private readonly IBaseRepository<SysUser> _baseRepository;

        public CreateUserCommandHandler(IMediator mediator,
            ILogger<CreateUserCommandHandler> logger,
            IBaseRepository<SysUser> baseRepository)
        {
            _mediator = mediator ?? throw new ArgumentNullException(nameof(IMediator));
            _logger = logger ?? throw new ArgumentNullException(nameof(ILogger<CreateUserCommandHandler>));
            _baseRepository = baseRepository ?? throw new ArgumentNullException(nameof(IBaseRepository<SysUser>));

        }

        public Task<MsgModel> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var sysUser = new SysUser
            {
                UserName = request.UserName,

            };
            throw new NotImplementedException();
        }
    }
}
