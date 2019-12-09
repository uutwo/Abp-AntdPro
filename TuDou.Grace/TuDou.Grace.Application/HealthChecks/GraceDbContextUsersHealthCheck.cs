using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;
using TuDou.Grace.EntityFrameworkCore;

namespace TuDou.Grace.HealthChecks
{
    public class GraceDbContextUsersHealthCheck : IHealthCheck
    {
        private readonly IDbContextProvider<GraceDbContext> _dbContextProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;
        public GraceDbContextUsersHealthCheck(
         IDbContextProvider<GraceDbContext> dbContextProvider,
         IUnitOfWorkManager unitOfWorkManager
         )
        {
            _dbContextProvider = dbContextProvider;
            _unitOfWorkManager = unitOfWorkManager;
        }
        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = default)
        {
            try
            {
                using (var uow = _unitOfWorkManager.Begin())
                {
                    // Switching to host is necessary for single tenant mode.
                    using (_unitOfWorkManager.Current.SetTenantId(null))
                    {
                        if (!await _dbContextProvider.GetDbContext().Database.CanConnectAsync(cancellationToken))
                        {
                            return HealthCheckResult.Unhealthy(
                                "AbpZeroTemplateDbContext could not connect to database"
                            );
                        }

                        var user = await _dbContextProvider.GetDbContext().Users.AnyAsync(cancellationToken);
                        uow.Complete();

                        if (user)
                        {
                            return HealthCheckResult.Healthy("AbpZeroTemplateDbContext connected to database and checked whether user added");
                        }

                        return HealthCheckResult.Unhealthy("AbpZeroTemplateDbContext connected to database but there is no user.");

                    }
                }
            }
            catch (Exception e)
            {
                return HealthCheckResult.Unhealthy("AbpZeroTemplateDbContext could not connect to database.", e);
            }
        }
    }
}
