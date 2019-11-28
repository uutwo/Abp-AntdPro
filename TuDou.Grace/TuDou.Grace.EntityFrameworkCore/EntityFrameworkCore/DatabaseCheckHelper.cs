using Abp.Dependency;
using Abp.Domain.Uow;
using Abp.EntityFrameworkCore;
using Abp.Extensions;
using Microsoft.EntityFrameworkCore;

namespace TuDou.Grace.EntityFrameworkCore
{
    public class DatabaseCheckHelper : ITransientDependency
    {
        private readonly IDbContextProvider<GraceDbContext> _dbContextProvider;
        private readonly IUnitOfWorkManager _unitOfWorkManager;

        public DatabaseCheckHelper(
            IDbContextProvider<GraceDbContext> dbContextProvider,
            IUnitOfWorkManager unitOfWorkManager
        )
        {
            _dbContextProvider = dbContextProvider;
            _unitOfWorkManager = unitOfWorkManager;
        }

        public bool Exist(string connectionString)
        {
            if (connectionString.IsNullOrEmpty())
            {
                //connectionString对于单元测试是空的
                return true;
            }

            try
            {
                using (var uow =_unitOfWorkManager.Begin())
                {
                    //单租户模式需要切换到主机。
                    using (_unitOfWorkManager.Current.SetTenantId(null))
                    {
                        _dbContextProvider.GetDbContext().Database.OpenConnection();
                        uow.Complete();
                    }
                }
            }
            catch
            {
                return false;
            }

            return true;
        }
    }
}
