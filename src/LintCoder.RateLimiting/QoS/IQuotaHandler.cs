using LintCoder.RateLimiting.QoS.Config;
using System.Threading.Tasks;

namespace LintCoder.RateLimiting.QoS
{
    public interface IQuotaHandler
    {
        LimitRuleType RuleType { get; }

        Task<(bool isAllow, int waittimeMills)> IsAllowRequestAsync(string requestIdentity, QuotaConfig config);
    }
}