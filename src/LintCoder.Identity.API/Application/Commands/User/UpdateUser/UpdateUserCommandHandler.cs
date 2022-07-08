using Common.Utility.Models;
using Lintcoder.Base;
using LintCoder.Identity.Domain.Entities;
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

        public Task<MsgModel> Handle(UpdateUserCommand request, CancellationToken cancellationToken)
        {
            throw new NotImplementedException();
        }
    }
}