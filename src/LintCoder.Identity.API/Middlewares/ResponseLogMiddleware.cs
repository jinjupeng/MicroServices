using LintCoder.Identity.API.Infrastructure.Attributes;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IO;
using System.Text;
using System.Text.Json;

namespace LintCoder.Identity.API.Middlewares
{
    /// <summary>
    /// Response Log 使用的 Middleware
    /// </summary>
    public class ResponseLogMiddleware : IMiddleware
    {
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly ILogger _logger;

        public ResponseLogMiddleware(ILoggerFactory loggerFactory)
        {
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            _logger = loggerFactory.CreateLogger<ResponseLogMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var originalBodyStream = context.Response.Body;
            await using var responseBody = _recyclableMemoryStreamManager.GetStream();
            context.Response.Body = responseBody;

            // 流入 pipeline
            await next(context);
            // 流出 pipeline

            context.Response.Body.Seek(0, SeekOrigin.Begin);
            var responseBodyTxt = await new StreamReader(context.Response.Body).ReadToEndAsync();
            context.Response.Body.Seek(0, SeekOrigin.Begin);
            await responseBody.CopyToAsync(originalBodyStream);

            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var apiLogAttribute = endpoint?.Metadata.GetMetadata<ApiLogAttribute>();
            var ignoreApiLogAttribute = endpoint?.Metadata.GetMetadata<IgnoreApiLogAttribute>();

            if (apiLogAttribute == null || ignoreApiLogAttribute != null)
            {
                var apiLogId = context.Items["ApiLogId"] ?? "";
                var apiLogInfo = new ApiLogInfo
                {
                    LogId = Convert.ToString(apiLogId) ?? "",
                    Scheme = context.Request.Scheme,
                    Host = context.Request.Host.ToUriComponent(),
                    Path = context.Request.Path,
                    QueryString = context.Request.QueryString.Value ?? "",
                    RequestHeader = GetHeaders(context.Response.Headers),
                    RequestBody = responseBodyTxt,
                    ResponseStatus = context.Response.StatusCode
                };

                _logger.LogInformation(JsonSerializer.Serialize(apiLogInfo));
            }
        }

        private static string GetHeaders(IHeaderDictionary headers)
        {
            var headerStr = new StringBuilder();
            foreach (var header in headers)
            {
                headerStr.Append($"{header.Key}: {header.Value}。");
            }

            return headerStr.ToString();
        }
    }

    /// <summary>
    /// 建立 Extension 将此 ResponseLogMiddleware 加入 HTTP pipeline
    /// </summary>
    public static class ResponseLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseResponseLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<ResponseLogMiddleware>();
        }
    }
}
