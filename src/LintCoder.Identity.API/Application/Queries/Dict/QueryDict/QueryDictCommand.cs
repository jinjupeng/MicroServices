using LintCoder.Identity.API.Application.Models.Request;
using LintCoder.Identity.API.Application.Models.Response;
using MediatR;

namespace LintCoder.Identity.API.Application.Queries.Dict.QueryDict
{
    public class QueryDictCommand : QueryModel, IRequest<MsgModel>
    {
        public string GroupCode { get; set; }

        public string GroupName { get; set; }
    }
}
