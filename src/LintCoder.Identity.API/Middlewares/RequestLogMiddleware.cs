using LintCoder.Identity.API.Infrastructure.Attributes;
using Microsoft.AspNetCore.Http.Features;
using Microsoft.IO;
using System.Text;
using System.Text.Json;

namespace LintCoder.Identity.API.Middlewares
{
    /// <summary>
    /// Request Log 使用的 Middleware
    /// </summary>
    public class RequestLogMiddleware : IMiddleware
    {
        private readonly RecyclableMemoryStreamManager _recyclableMemoryStreamManager;
        private readonly ILogger _logger;

        public RequestLogMiddleware(ILoggerFactory loggerFactory)
        {
            _recyclableMemoryStreamManager = new RecyclableMemoryStreamManager();
            _logger = loggerFactory.CreateLogger<RequestLogMiddleware>();
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            var endpoint = context.Features.Get<IEndpointFeature>()?.Endpoint;
            var apiLogAttribute = endpoint?.Metadata.GetMetadata<ApiLogAttribute>();
            var ignoreApiLogAttribute = endpoint?.Metadata.GetMetadata<IgnoreApiLogAttribute>();

            if (apiLogAttribute == null || ignoreApiLogAttribute != null)
            {
                // 无需记录log
                await next(context);
            }
            else
            {
                context.Request.EnableBuffering();
                await using var requestStream = _recyclableMemoryStreamManager.GetStream();
                await context.Request.Body.CopyToAsync(requestStream);
                var apiLogEventList = apiLogAttribute.ApiLogEvents.ToList();
                var logId = GetLogId();

                // 生成唯一的LogId，用来串联RequestLog和ResponseLog
                context.Items["ApiLogId"] = logId;
                var apiLogInfo = new ApiLogInfo
                {
                    LogId = logId,
                    Scheme = context.Request.Scheme,
                    Host = context.Request.Host.ToUriComponent(),
                    Path = context.Request.Path,
                    QueryString = context.Request.QueryString.ToString(),
                    RequestHeader = GetHeaders(context.Request.Headers),
                    RequestBody = ReadStreamInChunks(requestStream)
                };
                var apiLogJson = JsonSerializer.Serialize(apiLogEventList);

                if (apiLogEventList.Contains(ApiLogEvent.WriteToConsole))
                {
                    _logger.Log(apiLogAttribute.LogLevel, new EventId(), apiLogInfo, null, (x, y) => x.LogId);
                }

                context.Request.Body.Position = 0;
                await next(context);
            }
        }

        private static string GetLogId()
        {
            // DateTime(yyyyMMddhhmmssfff) + 1-UpperCase + 2-Digits

            var random = new Random();
            var idBuild = new StringBuilder();
            idBuild.Append(DateTime.Now.ToString("yyyyMMddhhmmssfff"));
            idBuild.Append((char)random.Next('A', 'A' + 26));
            idBuild.Append(random.Next(10, 99));
            return idBuild.ToString();
        }

        private static string ReadStreamInChunks(Stream stream)
        {
            const int readChunkBufferLength = 4096;
            stream.Seek(0, SeekOrigin.Begin);
            using var textWriter = new StringWriter();
            using var reader = new StreamReader(stream);
            var readChunk = new char[readChunkBufferLength];
            int readChunkLength;
            do
            {
                readChunkLength = reader.ReadBlock(readChunk, 0, readChunkBufferLength);
                textWriter.Write(readChunk, 0, readChunkLength);
            } while (readChunkLength > 0);
            return textWriter.ToString();
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

    public class ApiLogInfo
    {
        public string LogId { get; set; }

        public string Scheme { get; set; }

        public string Host { get; set; }

        public string Path { get; set; }

        public string QueryString { get; set; }

        public string RequestHeader { get; set; }

        public string RequestBody { get; set; }

        public int ResponseStatus { get; set; }
    }

    /// <summary>
    /// 建立 Extension 将此 RequestLogMiddleware 加入 HTTP pipeline
    /// </summary>
    public static class RequestLogMiddlewareExtensions
    {
        public static IApplicationBuilder UseRequestLogMiddleware(this IApplicationBuilder builder)
        {
            return builder.UseMiddleware<RequestLogMiddleware>();
        }
    }
}
