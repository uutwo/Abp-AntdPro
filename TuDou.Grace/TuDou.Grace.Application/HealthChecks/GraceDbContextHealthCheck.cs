using Microsoft.Extensions.Diagnostics.HealthChecks;
using System.Threading;
using System.Threading.Tasks;
using TuDou.Grace.EntityFrameworkCore;

namespace TuDou.Grace.HealthChecks
{
    public class GraceDbContextHealthCheck : IHealthCheck
    {
        private readonly DatabaseCheckHelper _checkHelper;
        public GraceDbContextHealthCheck(DatabaseCheckHelper checkHelper)
        {
            _checkHelper = checkHelper;
        }

        public Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            if (_checkHelper.Exist("db"))
            {
                return Task.FromResult(HealthCheckResult.Healthy("GraceDbContent connected to database."));
            }

            return Task.FromResult(HealthCheckResult.Unhealthy("GraceDbContent could not connect to database"));
        }
    }
}
