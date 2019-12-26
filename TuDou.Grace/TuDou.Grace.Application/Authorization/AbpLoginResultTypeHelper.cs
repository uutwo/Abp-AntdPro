using System;
using Abp.Authorization;
using Abp.Authorization.Users;
using Abp.Dependency;
using Abp.UI;

namespace TuDou.Grace.Authorization
{
    public class AbpLoginResultTypeHelper : GraceServiceBase, ITransientDependency
    {
        public Exception CreateExceptionForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    return new Exception("成功不要调用此方法!");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return new UserFriendlyException(L("LoginFailed"), L("InvalidUserNameOrPassword"));
                case AbpLoginResultType.InvalidTenancyName:
                    return new UserFriendlyException(L("LoginFailed"), L("ThereIsNoTenantDefinedWithName{0}", tenancyName));
                case AbpLoginResultType.TenantIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("TenantIsNotActive", tenancyName));
                case AbpLoginResultType.UserIsNotActive:
                    return new UserFriendlyException(L("LoginFailed"), L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress));
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return new UserFriendlyException(L("LoginFailed"), L("UserEmailIsNotConfirmedAndCanNotLogin"));
                case AbpLoginResultType.LockedOut:
                    return new UserFriendlyException(L("LoginFailed"), L("UserLockedOutMessage"));
                default: //实际上不能违约。但是将来可能会添加其他结果类型，我们可能会忘记处理它
                    Logger.Warn("未处理登录失败原因:" + result);
                    return new UserFriendlyException(L("LoginFailed"));
            }
        }

        public string CreateLocalizedMessageForFailedLoginAttempt(AbpLoginResultType result, string usernameOrEmailAddress, string tenancyName)
        {
            switch (result)
            {
                case AbpLoginResultType.Success:
                    throw new Exception("成功不要调用此方法");
                case AbpLoginResultType.InvalidUserNameOrEmailAddress:
                case AbpLoginResultType.InvalidPassword:
                    return L("InvalidUserNameOrPassword");
                case AbpLoginResultType.InvalidTenancyName:
                    return L("ThereIsNoTenantDefinedWithName{0}", tenancyName);
                case AbpLoginResultType.TenantIsNotActive:
                    return L("TenantIsNotActive", tenancyName);
                case AbpLoginResultType.UserIsNotActive:
                    return L("UserIsNotActiveAndCanNotLogin", usernameOrEmailAddress);
                case AbpLoginResultType.UserEmailIsNotConfirmed:
                    return L("UserEmailIsNotConfirmedAndCanNotLogin");
                case AbpLoginResultType.LockedOut:
                    return L("UserLockedOutMessage");
                default: //实际上不能违约。但是将来可能会添加其他结果类型，我们可能会忘记处理它
                    Logger.Warn("Unhandled login fail reason: " + result);
                    return L("LoginFailed");
            }
        }
    }
}
