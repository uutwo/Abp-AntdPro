using System;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp.Runtime.Caching;
using Abp.Runtime.Security;
using Abp.Runtime.Session;
using Abp.UI;
using TuDou.Grace.Authorization.Users;

namespace TuDou.Grace.Authorization.Impersonation
{
    public class ImpersonationManager : GraceDomainServiceBase, IImpersonationManager
    {
        public IAbpSession AbpSession { get; set; }

        private readonly ICacheManager _cacheManager;
        private readonly UserManager _userManager;
        private readonly UserClaimsPrincipalFactory _principalFactory;

        public ImpersonationManager(
            ICacheManager cacheManager,
            UserManager userManager,
            UserClaimsPrincipalFactory principalFactory)
        {
            _cacheManager = cacheManager;
            _userManager = userManager;
            _principalFactory = principalFactory;

            AbpSession = NullAbpSession.Instance;
        }

        public async Task<UserAndIdentity> GetImpersonatedUserAndIdentity(string impersonationToken)
        {
            var cacheItem = await _cacheManager.GetImpersonationCache().GetOrDefaultAsync(impersonationToken);
            if (cacheItem == null)
            {
                throw new UserFriendlyException(L("ImpersonationTokenErrorMessage"));
            }

            CheckCurrentTenant(cacheItem.TargetTenantId);

            //从租户获取用户
            var user = await _userManager.FindByIdAsync(cacheItem.TargetUserId.ToString());

            //创建身份

            var identity = (ClaimsIdentity)(await _principalFactory.CreateAsync(user)).Identity;

            if (!cacheItem.IsBackToImpersonator)
            {
                //添加审计日志的声明
                if (cacheItem.ImpersonatorTenantId.HasValue)
                {
                    identity.AddClaim(new Claim(AbpClaimTypes.ImpersonatorTenantId, cacheItem.ImpersonatorTenantId.Value.ToString(CultureInfo.InvariantCulture)));
                }

                identity.AddClaim(new Claim(AbpClaimTypes.ImpersonatorUserId, cacheItem.ImpersonatorUserId.ToString(CultureInfo.InvariantCulture)));
            }

            //删除缓存项以防止重复使用
            await _cacheManager.GetImpersonationCache().RemoveAsync(impersonationToken);

            return new UserAndIdentity(user, identity);
        }

        public Task<string> GetImpersonationToken(long userId, int? tenantId)
        {
            if (AbpSession.ImpersonatorUserId.HasValue)
            {
                throw new UserFriendlyException(L("CascadeImpersonationErrorMessage"));
            }

            if (AbpSession.TenantId.HasValue)
            {
                if (!tenantId.HasValue)
                {
                    throw new UserFriendlyException(L("FromTenantToHostImpersonationErrorMessage"));
                }

                if (tenantId.Value != AbpSession.TenantId.Value)
                {
                    throw new UserFriendlyException(L("DifferentTenantImpersonationErrorMessage"));
                }
            }

            return GenerateImpersonationTokenAsync(tenantId, userId, false);
        }

        public Task<string> GetBackToImpersonatorToken()
        {
            if (!AbpSession.ImpersonatorUserId.HasValue)
            {
                throw new UserFriendlyException(L("NotImpersonatedLoginErrorMessage"));
            }

            return GenerateImpersonationTokenAsync(AbpSession.ImpersonatorTenantId, AbpSession.ImpersonatorUserId.Value, true);
        }

        private void CheckCurrentTenant(int? tenantId)
        {
            if (AbpSession.TenantId != tenantId)
            {
                throw new Exception($"Current tenant is different than given tenant. AbpSession.TenantId: {AbpSession.TenantId}, given tenantId: {tenantId}");
            }
        }

        private async Task<string> GenerateImpersonationTokenAsync(int? tenantId, long userId, bool isBackToImpersonator)
        {
            //创建缓存项
            var cacheItem = new ImpersonationCacheItem(
                tenantId,
                userId,
                isBackToImpersonator
            );

            if (!isBackToImpersonator)
            {
                cacheItem.ImpersonatorTenantId = AbpSession.TenantId;
                cacheItem.ImpersonatorUserId = AbpSession.GetUserId();
            }

            //创建一个随机令牌并保存到缓存中
            var token = Guid.NewGuid().ToString();

            await _cacheManager
                .GetImpersonationCache()
                .SetAsync(token, cacheItem, TimeSpan.FromMinutes(1));

            return token;
        }
    }
}
