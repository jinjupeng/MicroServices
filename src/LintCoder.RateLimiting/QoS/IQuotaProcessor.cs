using LintCoder.RateLimiting.QoS.Config;
using System.Threading.Tasks;

namespace LintCoder.RateLimiting.QoS
{
    public interface IQuotaProcessor
    {
        Task ProcessAsync(string requestIdentity, QuotaConfig quotaConfig);
    }
}