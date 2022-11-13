using LintCoder.RateLimiting.QoS.Config;

namespace LintCoder.RateLimiting.QoS.Rules
{
    public interface IRuleMatcher
    {
        bool IsMatched(RequestInfo requestInfo, out QoSConfig qoSConfig);
    }
}