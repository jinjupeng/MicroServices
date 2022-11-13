using LintCoder.RateLimiting.QoS.Config;
using System;
using System.Threading.Tasks;

namespace LintCoder.RateLimiting.QoS.Services
{
    public interface ILockerService
    {
        Task<(bool IsSuccess, IDisposable Locker)> TryTakeLockAsync(string requestIdentity, LockerConfig lockerConfig);
    }
}