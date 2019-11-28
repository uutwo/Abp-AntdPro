using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using TuDou.Grace.Configuration;
using TuDou.Grace.Web;

namespace TuDou.Grace.EntityFrameworkCore
{
    /* 这个类需要在开发时从命令行运行“dotnet ef…”命令。其他地方没用过 */
    public class GraceDbContextFactory : IDesignTimeDbContextFactory<GraceDbContext>
    {
        public GraceDbContext CreateDbContext(string[] args)
        {
            var builder = new DbContextOptionsBuilder<GraceDbContext>();
            var configuration = AppConfigurations.Get(WebContentDirectoryFinder.CalculateContentRootFolder(), addUserSecrets: true);

            GraceDbContextConfigurer.Configure(builder, configuration.GetConnectionString(GraceConsts.ConnectionStringName));

            return new GraceDbContext(builder.Options);
        }
    }
}