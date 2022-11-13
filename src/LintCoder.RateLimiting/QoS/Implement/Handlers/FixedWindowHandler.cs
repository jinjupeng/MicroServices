using LintCoder.RateLimiting.QoS.Config;
using LintCoder.RateLimiting.QoS.Counters;
using LintCoder.RateLimiting.QoS.Services;
using System;
using System.Threading.Tasks;

namespace LintCoder.RateLimiting.QoS.Implement.Handlers
{
    public class FixedWindowHandler : BaseQuotaHandler, IQuotaHandler
    {
        private readonly ICounterStoreService _counterStoreService;

        public FixedWindowHandler(ICounterStoreService counterStoreService, ILockerService lockerService) : base(lockerService)
        {
            _counterStoreService = counterStoreService;
        }

        public LimitRuleType RuleType => LimitRuleType.FixedWindow;

        public async Task<(bool isAllow, int waittimeMills)> IsAllowRequestAsync(string requestIdentity, QuotaConfig config)
        {
            using (await GetLockerAsync(requestIdentity, config.Locker))
            {
                var windowStartTime = GetLastMaxTime(config);
                //获取对应的窗口计数器
                var (IsSuccess, Counter) = await _counterStoreService.TryGetCounterAsync<WindowCounter>($"{RuleType}:{requestIdentity}");

                if (!IsSuccess || Counter.StartTime < windowStartTime)
                {
                    Counter = new WindowCounter(windowStartTime, config.PeriodCount, config.PeriodTimeSpan);
                }
                if (Counter.Count >= Counter.LimitPeriodCount)
                {
                    return (false, 0);
                }
                Counter.Count++;
                await _counterStoreService.SetCounterAsync($"{RuleType}:{requestIdentity}", Counter, TimeSpan.FromMilliseconds(Counter.LimitPeriodMillSeconds + 10000));
                return (true, 0);
            }
        }
    }
}