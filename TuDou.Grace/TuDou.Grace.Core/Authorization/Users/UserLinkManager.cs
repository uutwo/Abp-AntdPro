using System;
using System.Collections.Generic;
using System.Globalization;
using System.Security.Claims;
using System.Threading.Tasks;
using Abp;
using Abp.Authorization.Users;
using Abp.Domain.Repositories;
using Abp.Domain.Uow;
using Abp.Runtime.Caching;
using Abp.Runtime.Security;
using Abp.Runtime.Session;
using Abp.UI;
using TuDou.Grace.Authorization.Impersonation;

namespace TuDou.Grace.Authorization.Users
{
    public class UserLinkManager : GraceDomainServiceBase, IUserLinkManager
    {
        private readonly IRepository<UserAccount, long> _userAccountRepository;
        private readonly ICacheManager _cacheManager;
        private readonly UserManager _userManager;
        private readonly UserClaimsPrincipalFactory _principalFactory;

        public IAbpSession AbpSession { get; set; }

        public UserLinkManager(
            IRepository<UserAccount, long> userAccountRepository,
            ICacheManager cacheManager, 
            UserManager userManager,
            UserClaimsPrincipalFactory principalFactory)
        {
            _userAccountRepository = userAccountRepository;
            _cacheManager = cacheManager;
            _userManager = userManager;
            _principalFactory = principalFactory;

            AbpSession = NullAbpSession.Instance;
        }

        [UnitOfWork]
        public virtual async Task Link(User firstUser, User secondUser)
        {
            var firstUserAccount = await GetUserAccountAsync(firstUser.ToUserIdentifier());
            var secondUserAccount = await GetUserAccountAsync(secondUser.ToUserIdentifier());

            var userLinkId = firstUserAccount.UserLinkId ?? firstUserAccount.Id;
            firstUserAccount.UserLinkId = userLinkId;

            var userAccountsToLink = secondUserAccount.UserLinkId.HasValue
                ? _userAccountRepository.GetAllList(ua => ua.UserLinkId == secondUserAccount.UserLinkId.Value)
                : new List<UserAccount> { secondUserAccount };

            userAccountsToLink.ForEach(u =>
            {
                u.UserLinkId = userLinkId;
            });

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [UnitOfWork]
        public virtual async Task<bool> AreUsersLinked(UserIdentifier firstUserIdentifier, UserIdentifier secondUserIdentifier)
        {
            var firstUserAccount = await GetUserAccountAsync(firstUserIdentifier);
            var secondUserAccount = await GetUserAccountAsync(secondUserIdentifier);

            if (!firstUserAccount.UserLinkId.HasValue || !secondUserAccount.UserLinkId.HasValue)
            {
                return false;
            }

            return firstUserAccount.UserLinkId == secondUserAccount.UserLinkId;
        }

        [UnitOfWork]
        public virtual async Task Unlink(UserIdentifier userIdentifier)
        {
            var targetUserAccount = await GetUserAccountAsync(userIdentifier);
            targetUserAccount.UserLinkId = null;

            await CurrentUnitOfWork.SaveChangesAsync();
        }

        [UnitOfWork]
        public virtual async Task<UserAccount> GetUserAccountAsync(UserIdentifier userIdentifier)
        {
            return await _userAccountRepository.FirstOrDefaultAsync(ua => ua.TenantId == userIdentifier.TenantId && ua.UserId == userIdentifier.UserId);
        }

        public async Task<string> GetAccountSwitchToken(long targetUserId, int? targetTenantId)
        {
            //创建缓存项
            var cacheItem = new SwitchToLinkedAccountCacheItem(
                targetTenantId,
                targetUserId,
                AbpSession.ImpersonatorTenantId,
                AbpSession.ImpersonatorUserId
            );

            //创建一个随机令牌并保存到缓存中
            var token = Guid.NewGuid().ToString();

            await _cacheManager
                .GetSwitchToLinkedAccountCache()
                .SetAsync(token, cacheItem, TimeSpan.FromMinutes(1));

            return token;
        }

        public async Task<UserAndIdentity> GetSwitchedUserAndIdentity(string switchAccountToken)
        {
            var cacheItem = await _cacheManager.GetSwitchToLinkedAccountCache().GetOrDefaultAsync(switchAccountToken);
            if (cacheItem == null)
            {
                throw new UserFriendlyException(L("SwitchToLinkedAccountTokenErrorMessage"));
            }

            //从承租者获取用户
            var user = await _userManager.FindByIdAsync(cacheItem.TargetUserId.ToString());

            //创建身份
            var identity = (ClaimsIdentity)(await _principalFactory.CreateAsync(user)).Identity;

            //添加审计日志的声明
            if (cacheItem.ImpersonatorTenantId.HasValue)
            {
                identity.AddClaim(new Claim(AbpClaimTypes.ImpersonatorTenantId, cacheItem.ImpersonatorTenantId.Value.ToString(CultureInfo.InvariantCulture)));
            }

            if (cacheItem.ImpersonatorUserId.HasValue)
            {
                identity.AddClaim(new Claim(AbpClaimTypes.ImpersonatorUserId, cacheItem.ImpersonatorUserId.Value.ToString(CultureInfo.InvariantCulture)));
            }

            //删除缓存项以防止重复使用
            await _cacheManager.GetSwitchToLinkedAccountCache().RemoveAsync(switchAccountToken);

            return new UserAndIdentity(user, identity);
        }
    }
}
