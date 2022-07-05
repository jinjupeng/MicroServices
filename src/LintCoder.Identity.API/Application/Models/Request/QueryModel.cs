namespace LintCoder.Identity.API.Application.Models.Request
{
    public class QueryModel
    {
        public int PageNum { get; set; }

        public int PageSize { get; set; }

        public string Keyword { get; set; }

    }
}
