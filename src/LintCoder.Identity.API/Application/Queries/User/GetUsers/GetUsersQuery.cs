using LintCoder.Identity.API.Application.Models.Response;
using MediatR;

namespace LintCoder.Identity.API.Application.Queries.User.GetUsers
{
    public class GetUsersQuery : IRequest<List<SysUserResponse>>
    {
    }
}
