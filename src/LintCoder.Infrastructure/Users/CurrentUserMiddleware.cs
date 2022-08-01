using LintCoder.Application.Common.Interfaces;
using Microsoft.AspNetCore.Http;

namespace LintCoder.Infrastructure
{
    public class CurrentUserMiddleware : IMiddleware
    {
        private readonly ICurrentUserInitializer _currentUserInitializer;

        public CurrentUserMiddleware(ICurrentUserInitializer currentUserInitializer) =>
            _currentUserInitializer = currentUserInitializer;

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            _currentUserInitializer.SetCurrentUser(context.User);

            await next(context);
        }
    }
}
