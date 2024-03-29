﻿using static LintCoder.RateLimiting.QoS.Delegates.QoSEvents;

namespace LintCoder.RateLimiting.QoS.Options
{
    public class QoSOptionsBuilder
    {
        public RuleResetEventDelegate RuleResetEvent { get; set; }

        public QoSOnBreakDelegate OnBreakEvent { get; set; }

        public QoSOnResetDelegate OnResetEvent { get; set; }

        public QoSOnHalfOpenDelegate OnHalfOpen { get; set; }

        public LimitProcessResultListener OnLimitProcessResult { get; set; }

        public FallbackActionDelegate OnFallbackAction { get; set; }

        public OnFallbackDelegate OnFallback { get; set; }

        public PolicyBuilderDelegate FallbackBuilderConfigure { get; set; }
    }
}