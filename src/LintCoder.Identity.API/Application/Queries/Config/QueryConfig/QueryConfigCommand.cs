using LintCoder.Identity.API.Application.Models.Request;
using LintCoder.Identity.API.Application.Models.Response;
using MediatR;

namespace LintCoder.Identity.API.Application.Queries.Config.QueryConfig
{
    public class QueryConfigCommand : QueryModel, IRequest<MsgModel>
    {
    }
}
