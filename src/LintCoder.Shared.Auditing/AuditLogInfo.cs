using LintCoder.Base.Entities;
using System.Text;

namespace LintCoder.Shared.Auditing
{
    [Serializable]
    public class AuditLogInfo
    {
        public string ApplicationName { get; set; }

        public Guid? UserId { get; set; }

        public string UserName { get; set; }

        public DateTime ExecutionTime { get; set; }

        /// <summary>
        /// 执行时长：单位（毫秒）
        /// </summary>
        public long ExecutionDuration { get; set; }

        public string CorrelationId { get; set; }

        public string ClientIpAddress { get; set; }

        public string HttpMethod { get; set; }

        public int? HttpStatusCode { get; set; }

        public string Url { get; set; }

        public override string ToString()
        {
            var sb = new StringBuilder();

            sb.AppendLine($"AUDIT LOG: [{HttpStatusCode?.ToString() ?? "---"}: {(HttpMethod ?? "-------").PadRight(7)}] {Url}");
            sb.AppendLine($"- UserName - UserId                 : {UserName} - {UserId}");
            sb.AppendLine($"- ClientIpAddress        : {ClientIpAddress}");
            sb.AppendLine($"- ExecutionDuration      : {ExecutionDuration}");

            return sb.ToString();
        }
    }
}
