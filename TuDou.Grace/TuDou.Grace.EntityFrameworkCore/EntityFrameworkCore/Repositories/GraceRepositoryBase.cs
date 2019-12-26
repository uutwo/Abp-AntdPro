using Abp.Domain.Entities;
using Abp.EntityFrameworkCore;
using Abp.EntityFrameworkCore.Repositories;

namespace TuDou.Grace.EntityFrameworkCore.Repositories
{
    /// <summary>
    /// 应用程序自定义存储库的基类。
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    /// <typeparam name="TPrimaryKey">Primary key type of the entity</typeparam>
    public abstract class GraceRepositoryBase<TEntity, TPrimaryKey> : EfCoreRepositoryBase<GraceDbContext, TEntity, TPrimaryKey>
        where TEntity : class, IEntity<TPrimaryKey>
    {
        protected GraceRepositoryBase(IDbContextProvider<GraceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

        //添加所有存储库的公共方法
    }

    /// <summary>
    /// 应用程序自定义存储库的基类。
    /// This is a shortcut of <see cref="GraceRepositoryBase{TEntity,TPrimaryKey}"/> for <see cref="int"/> primary key.
    /// </summary>
    /// <typeparam name="TEntity">Entity type</typeparam>
    public abstract class GraceRepositoryBase<TEntity> : GraceRepositoryBase<TEntity, int>
        where TEntity : class, IEntity<int>
    {
        protected GraceRepositoryBase(IDbContextProvider<GraceDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }

    }
}
