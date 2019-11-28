using System;
using System.IO;
using System.Text;
using Abp.AspNetCore;
using Abp.AspNetCore.Configuration;
using Abp.AspNetCore.SignalR;
using Abp.Configuration.Startup;
using Abp.Hangfire;
using Abp.Hangfire.Configuration;
using Abp.Modules;
using Abp.Reflection.Extensions;
using Abp.Runtime.Caching.Redis;
using Abp.Zero.Configuration;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TuDou.Grace.Configuration;
using TuDou.Grace.EntityFrameworkCore;
using TuDou.Grace.Startup;
using TuDou.Grace.Web.Authentication.JwtBearer;
using TuDou.Grace.Web.Authentication.TwoFactor;
using TuDou.Grace.Web.Configuration;

namespace TuDou.Grace.Web
{
    [DependsOn(
        typeof(GraceApplicationModule),
        typeof(GraceEntityFrameworkCoreModule),
        typeof(AbpAspNetCoreSignalRModule),
        typeof(AbpAspNetCoreModule),
        typeof(GraceGraphQLModule),
        typeof(AbpRedisCacheModule), //如果不使用Redis缓存，可以删除AbpRedisCacheModule依赖项(和Abp.RedisCache nuget包)
        typeof(AbpHangfireAspNetCoreModule) //AbpHangfireModule依赖(和Abp.Hangfire)。如果不使用Hangfire，可以删除AspNetCore nuget包)
    )]
    public class GraceWebCoreModule : AbpModule
    {
        private readonly IWebHostEnvironment _env;
        private readonly IConfigurationRoot _appConfiguration;

        public GraceWebCoreModule(IWebHostEnvironment env)
        {
            _env = env;
            _appConfiguration = env.GetAppConfiguration();
        }

        public override void PreInitialize()
        {
            //设置默认连接字符串
            Configuration.DefaultNameOrConnectionString = _appConfiguration.GetConnectionString(
                GraceConsts.ConnectionStringName
            );

            //使用数据库进行语言管理
            Configuration.Modules.Zero().LanguageManagement.EnableDbLocalization();

            Configuration.Modules.AbpAspNetCore()
                .CreateControllersForAppServices(
                    typeof(GraceApplicationModule).GetAssembly()
                );

            Configuration.Caching.Configure(TwoFactorCodeCacheItem.CacheName, cache =>
            {
                cache.DefaultAbsoluteExpireTime = TimeSpan.FromMinutes(2);
            });

            if (_appConfiguration["Authentication:JwtBearer:IsEnabled"] != null && bool.Parse(_appConfiguration["Authentication:JwtBearer:IsEnabled"]))
            {
                ConfigureTokenAuth();
            }

            Configuration.ReplaceService<IAppConfigurationAccessor, AppConfigurationAccessor>();

            //取消对这一行的注释，以使用Hangfire代替默认的后台作业管理器(还请记住取消对Startup.cs文件中相关行的注释)。
            Configuration.BackgroundJobs.UseHangfire();

            //取消注释这一行以使用Redis缓存而不是内存缓存。
            //有关Redis配置和连接字符串，请参阅app.config
            //Configuration.Caching.UseRedis(options =>
            //{
            //    options.ConnectionString = _appConfiguration["Abp:RedisCache:ConnectionString"];
            //    options.DatabaseId = _appConfiguration.GetValue<int>("Abp:RedisCache:DatabaseId");
            //});
        }

        private void ConfigureTokenAuth()
        {
            IocManager.Register<TokenAuthConfiguration>();
            var tokenAuthConfig = IocManager.Resolve<TokenAuthConfiguration>();

            tokenAuthConfig.SecurityKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(_appConfiguration["Authentication:JwtBearer:SecurityKey"]));
            tokenAuthConfig.Issuer = _appConfiguration["Authentication:JwtBearer:Issuer"];
            tokenAuthConfig.Audience = _appConfiguration["Authentication:JwtBearer:Audience"];
            tokenAuthConfig.SigningCredentials = new SigningCredentials(tokenAuthConfig.SecurityKey, SecurityAlgorithms.HmacSha256);
            tokenAuthConfig.AccessTokenExpiration = AppConsts.AccessTokenExpiration;
            tokenAuthConfig.RefreshTokenExpiration = AppConsts.RefreshTokenExpiration;
        }

        public override void Initialize()
        {
            IocManager.RegisterAssemblyByConvention(typeof(GraceWebCoreModule).GetAssembly());
        }

        public override void PostInitialize()
        {
            SetAppFolders();
        }

        private void SetAppFolders()
        {
            var appFolders = IocManager.Resolve<AppFolders>();

            appFolders.SampleProfileImagesFolder = Path.Combine(_env.WebRootPath, $"Common{Path.DirectorySeparatorChar}Images{Path.DirectorySeparatorChar}SampleProfilePics");
            appFolders.WebLogsFolder = Path.Combine(_env.ContentRootPath, $"App_Data{Path.DirectorySeparatorChar}Logs");

        }
    }
}
