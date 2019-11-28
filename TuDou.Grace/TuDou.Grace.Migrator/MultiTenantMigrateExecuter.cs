using System;
using System.Collections.Generic;
using Abp.Data;
using Abp.Dependency;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Extensions;
using Abp.MultiTenancy;
using Abp.Runtime.Security;
using TuDou.Grace.EntityFrameworkCore;
using TuDou.Grace.Migrations.Seed;
using TuDou.Grace.MultiTenancy;

namespace TuDou.Grace.Migrator
{
    public class MultiTenantMigrateExecuter : ITransientDependency
    {
        public Log Log { get; private set; }

        private readonly AbpZeroDbMigrator _migrator;
        private readonly IRepository<Tenant> _tenantRepository;
        private readonly IDbPerTenantConnectionStringResolver _connectionStringResolver;

        public MultiTenantMigrateExecuter(
            AbpZeroDbMigrator migrator, 
            IRepository<Tenant> tenantRepository,
            Log log,
            IDbPerTenantConnectionStringResolver connectionStringResolver)
        {
            Log = log;

            _migrator = migrator;
            _tenantRepository = tenantRepository;
            _connectionStringResolver = connectionStringResolver;
        }

        public void Run(bool skipConnVerification)
        {
            var hostConnStr = _connectionStringResolver.GetNameOrConnectionString(new ConnectionStringResolveArgs(MultiTenancySides.Host));
            if (hostConnStr.IsNullOrWhiteSpace())
            {
                Log.Write("配置文件应该包含一个名为“Default”的连接字符串");
                return;
            }

            Log.Write("主机数据库:" + ConnectionStringHelper.GetConnectionString(hostConnStr));
            if (!skipConnVerification)
            {
                Log.Write("继续为这个主机数据库和所有租户迁移?(Y / N):");
                var command = Console.ReadLine();
                if (!command.IsIn("Y", "y"))
                {
                    Log.Write("迁移取消。");
                    return;
                }
            }

            Log.Write("主机数据库开始迁移…");

            try
            {
                _migrator.CreateOrMigrateForHost(SeedHelper.SeedHostDb);
            }
            catch (Exception ex)
            {
                Log.Write("迁移主机数据库时出错:");
                Log.Write(ex.ToString());
                Log.Write("取消迁移。");
                return;
            }

            Log.Write("完成主机数据库迁移。");
            Log.Write("--------------------------------------------------------");

            var migratedDatabases = new HashSet<string>();
            var tenants = _tenantRepository.GetAllList(t => t.ConnectionString != null && t.ConnectionString != "");
            for (int i = 0; i < tenants.Count; i++)
            {
                var tenant = tenants[i];
                Log.Write(string.Format("租户数据库开始迁移({0} / {1})", (i + 1), tenants.Count));
                Log.Write("名称：              : " + tenant.Name);
                Log.Write("租户名称：       : " + tenant.TenancyName);
                Log.Write("租户 Id ：        : " + tenant.Id);
                Log.Write("连接字符串 : " + SimpleStringCipher.Instance.Decrypt(tenant.ConnectionString));

                if (!migratedDatabases.Contains(tenant.ConnectionString))
                {
                    try
                    {
                        _migrator.CreateOrMigrateForTenant(tenant);
                    }
                    catch (Exception ex)
                    {
                        Log.Write("租户数据库迁移过程中出现错误:");
                        Log.Write(ex.ToString());
                        Log.Write("跳过这个租户，并将继续为其他人……");
                    }

                    migratedDatabases.Add(tenant.ConnectionString);
                }
                else
                {
                    Log.Write("此数据库以前已经迁移过(同一数据库中有多个租户)。跳过它....");
                }

                Log.Write(string.Format("租户数据库迁移完成。 ({0} / {1})", (i + 1), tenants.Count));
                Log.Write("--------------------------------------------------------");
            }

            Log.Write("所有数据库迁移完成");
        }
    }
}