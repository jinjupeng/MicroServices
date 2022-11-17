using LintCoder.RateLimiting.QoS.Config;
using Polly;
using System.Collections.Generic;

namespace LintCoder.RateLimiting.QoS
{
    public interface IPollyFactory
    {
        IAsyncPolicy Get(string requestIdentity, BreakConfig qosConfig);

        Context CreatePollyContext(string requestIdentity, Dictionary<string, object> contextData = null);
    }
}
