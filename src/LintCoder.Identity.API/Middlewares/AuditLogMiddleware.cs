using LintCoder.Application.Common;
using LintCoder.Application.Users;
using LintCoder.Shared.Auditing;
using LintCoder.Shared.Auditing.WriteToMonogDB;
using Microsoft.AspNetCore.Http.Features;
using System.Diagnostics;
using System.Text;

namespace LintCoder.Identity.API.Middlewares
{
    public class AuditLogMiddleware : IMiddleware
    {
        private readonly IAuditingProvider<AuditLogMongoDBInfo> auditingProvider;
        private readonly ICurrentUser currentUser;

        public AuditLogMiddleware(IAuditingProvider<AuditLogMongoDBInfo> auditingProvider, ICurrentUser currentUser)
        {
            this.auditingProvider = auditingProvider ?? throw new ArgumentException($"{typeof(IAuditingProvider<AuditLogMongoDBInfo>)}服务未注册");
            this.currentUser = currentUser;
        }

        public async Task InvokeAsync(HttpContext context, RequestDelegate next)
        {
            try
            {
                var watch = Stopwatch.StartNew();
                EndpointMetadataCollection endpointMetaData = context.Features.Get<IEndpointFeature>()?.Endpoint.Metadata;

                context.Request.EnableBuffering();

                var auditLog = await LogRequest(context);
                auditLog = await LogResponse(context, next, auditLog);

                watch.Stop();
                var auditLogMongo = new AuditLogMongoDBInfo
                {
                    ApplicationName = auditLog.ApplicationName,
                    ClientIpAddress = auditLog.ClientIpAddress,
                    Url = auditLog.Url,
                    UserId = auditLog.UserId,
                    UserName = auditLog.UserName,
                    CorrelationId = auditLog.CorrelationId,
                    HttpMethod = auditLog.HttpMethod,
                    ExecutionTime = auditLog.ExecutionTime,
                    ExecutionDuration = watch.ElapsedMilliseconds,
                };

                await auditingProvider.AddAuditLogAsync(auditLogMongo);
            }
            catch (UnauthorizedAccessException)
            {
                throw;
            }
            catch (Exception ex)
            {
                //Custom exception logging here
            }
        }

        public async Task<AuditLogInfo> LogRequest(HttpContext context)
        {
            IHttpRequestFeature features = context.Features.Get<IHttpRequestFeature>();
            string url = $"{features.Scheme}://{context.Request.Host.Value}{features.RawTarget}";
            if (context.Request.HasFormContentType)
            {
                IFormCollection form = context.Request.Form;
            }
            else
            {
                string formString = await new StreamReader(context.Request.Body).ReadToEndAsync();
                var injectedRequestStream = new MemoryStream();
                byte[] bytesToWrite = Encoding.UTF8.GetBytes(formString);
                injectedRequestStream.Write(bytesToWrite, 0, bytesToWrite.Length);
                injectedRequestStream.Seek(0, SeekOrigin.Begin);
                context.Request.Body = injectedRequestStream;
            }

            return new AuditLogInfo()
            {
                ClientIpAddress = context?.Connection?.RemoteIpAddress?.ToString(),
                Url = url,
                UserId = currentUser.GetUserId(),
                UserName = currentUser.Name,
                ApplicationName = "LintCoder.Idenitty.API",
                CorrelationId = "",
                HttpMethod = context?.Request.Method,
                ExecutionTime = DateTime.Now
            };
        }

        public async Task<AuditLogInfo> LogResponse(HttpContext context, RequestDelegate next, AuditLogInfo auditLog)
        {
            if (auditLog == null)
            {
                await next(context);
                return auditLog;
            }

            Stream originalBody = context.Response.Body;

            try
            {

                using (var memStream = new MemoryStream())
                {
                    context.Response.Body = memStream;

                    await next(context);

                    memStream.Position = 0;
                    string responseBody = new StreamReader(memStream).ReadToEnd();

                    auditLog.HttpStatusCode = context.Response.StatusCode;

                    memStream.Position = 0;
                    await memStream.CopyToAsync(originalBody);
                }

                return auditLog;
            }
            catch
            {
                throw;
            }
            finally
            {
                context.Response.Body = originalBody;
            }
        }
    }
}
