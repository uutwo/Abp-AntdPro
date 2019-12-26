using System.IO;
using Abp.Dependency;
using Microsoft.Extensions.Configuration;

namespace TuDou.Grace.Configuration
{
    /* 该服务分别在Web层和测试项目中被替换 */
    public class DefaultAppConfigurationAccessor : IAppConfigurationAccessor, ISingletonDependency
    {
        public IConfigurationRoot Configuration { get; }
        
        public DefaultAppConfigurationAccessor()
        {
            Configuration = AppConfigurations.Get(Directory.GetCurrentDirectory());
        }
    }
}