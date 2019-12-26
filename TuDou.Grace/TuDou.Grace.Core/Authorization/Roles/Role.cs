using Abp.Authorization.Roles;
using TuDou.Grace.Authorization.Users;

namespace TuDou.Grace.Authorization.Roles
{
    /// <summary>
    /// 表示系统中的一个角色。
    /// </summary>
    public class Role : AbpRole<User>
    {
        //可以在这里添加应用程序特定的角色属性

        public Role()
        {
            
        }

        public Role(int? tenantId, string displayName)
            : base(tenantId, displayName)
        {

        }

        public Role(int? tenantId, string name, string displayName)
            : base(tenantId, name, displayName)
        {

        }
    }
}
