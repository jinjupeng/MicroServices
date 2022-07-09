using LintCoder.Identity.API.Application.Models.Response;
using MediatR;

namespace LintCoder.Identity.API.Application.Queries.User.QueryCurrentUser
{
    public class QueryCurrentUserCommand : IRequest<MsgModel>
    {
    }
}
