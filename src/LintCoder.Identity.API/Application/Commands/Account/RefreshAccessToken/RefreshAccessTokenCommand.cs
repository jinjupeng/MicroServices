using LintCoder.Identity.API.Application.Models.Response;
using MediatR;

namespace LintCoder.Identity.API.Application.Commands.Account.RefreshAccessToken
{
    public class RefreshAccessTokenCommand : IRequest<MsgModel>
    {
    }
}
