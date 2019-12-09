using Abp.Runtime.Caching;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace TuDou.Grace.HealthChecks
{
    public class CacheHealthCheck : IHealthCheck
    {
        private readonly ICacheManager _cacheManager;

        public CacheHealthCheck(ICacheManager cacheManager)
        {
            _cacheManager = cacheManager;
        }

        // 这个函数试图设置和获取缓存中的数据。
        // 如果启用了redis缓存，这将尝试连接到redis设置和获取缓存数据。如果它不会抛出一个异常，这意味着redis是起来和健康的。
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                var cacheManager = _cacheManager.GetCache("TestCache");

                var testKey = "Test-" + Guid.NewGuid();

                await cacheManager.SetAsync(testKey, "123");

                await cacheManager.GetOrDefaultAsync(testKey);

                return HealthCheckResult.Healthy("The cache check is healthy. (If you are using Redis, Redis is also checked)");
            }
            catch (Exception e)
            {
                return HealthCheckResult.Unhealthy("The cache check is unhealthy. (If you are using Redis, Redis is also checked)" + e.Message);
            }
        }
    }
}
