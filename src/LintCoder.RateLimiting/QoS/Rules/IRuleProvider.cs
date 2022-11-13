using LintCoder.RateLimiting.QoS.Config;
using LintCoder.RateLimiting.QoS.Options;
using System.Collections.Generic;

namespace LintCoder.RateLimiting.QoS.Rules
{
    public interface IRuleProvider
    {
        List<QoSConfig> GetConfigs();

        void InstallRules(Dictionary<string, QoSOptions> qoSOptions);
    }
}