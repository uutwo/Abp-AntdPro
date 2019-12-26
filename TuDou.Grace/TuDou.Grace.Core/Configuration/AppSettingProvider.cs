using System.Collections.Generic;
using System.Linq;
using Abp.Configuration;
using Abp.Net.Mail;
using Abp.Zero.Configuration;
using Microsoft.Extensions.Configuration;

namespace TuDou.Grace.Configuration
{
    /// <summary>
    /// 定义应用程序的设置。
    /// 有关设置名称，请参见<参见cref="AppSettings"/>。
    /// </summary>
    public class AppSettingProvider : SettingProvider
    {
        private readonly IConfigurationRoot _appConfiguration;

        public AppSettingProvider(IAppConfigurationAccessor configurationAccessor)
        {
            _appConfiguration = configurationAccessor.Configuration;
        }

        public override IEnumerable<SettingDefinition> GetSettingDefinitions(SettingDefinitionProviderContext context)
        {
            // 默认禁用TwoFactorLogin(可以通过UI启用)
            context.Manager.GetSettingDefinition(AbpZeroSettingNames.UserManagement.TwoFactorLogin.IsEnabled).DefaultValue = false.ToString().ToLowerInvariant();

            // 更改电子邮件设置的范围
            ChangeEmailSettingScopes(context);

            return GetHostSettings().Union(GetTenantSettings()).Union(GetSharedSettings())
                // 主题设置
                .Union(GetDefaultThemeSettings());
        }

        private void ChangeEmailSettingScopes(SettingDefinitionProviderContext context)
        {
            if (!GraceConsts.AllowTenantsToChangeEmailSettings)
            {
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.Host).Scopes = SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.Port).Scopes = SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.UserName).Scopes = SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.Password).Scopes = SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.Domain).Scopes = SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.EnableSsl).Scopes = SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.Smtp.UseDefaultCredentials).Scopes = SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.DefaultFromAddress).Scopes = SettingScopes.Application;
                context.Manager.GetSettingDefinition(EmailSettingNames.DefaultFromDisplayName).Scopes = SettingScopes.Application;
           
            }
        }

        private IEnumerable<SettingDefinition> GetHostSettings()
        {
            return new[] {
                new SettingDefinition(AppSettings.TenantManagement.AllowSelfRegistration, GetFromAppSettings(AppSettings.TenantManagement.AllowSelfRegistration, "true"), isVisibleToClients: true),
                new SettingDefinition(AppSettings.TenantManagement.IsNewRegisteredTenantActiveByDefault, GetFromAppSettings(AppSettings.TenantManagement.IsNewRegisteredTenantActiveByDefault, "false")),
                new SettingDefinition(AppSettings.TenantManagement.UseCaptchaOnRegistration, GetFromAppSettings(AppSettings.TenantManagement.UseCaptchaOnRegistration, "true"), isVisibleToClients: true),
                new SettingDefinition(AppSettings.TenantManagement.DefaultEdition, GetFromAppSettings(AppSettings.TenantManagement.DefaultEdition, "")),
                new SettingDefinition(AppSettings.UserManagement.SmsVerificationEnabled, GetFromAppSettings(AppSettings.UserManagement.SmsVerificationEnabled, "false"), isVisibleToClients: true),
                new SettingDefinition(AppSettings.TenantManagement.SubscriptionExpireNotifyDayCount, GetFromAppSettings(AppSettings.TenantManagement.SubscriptionExpireNotifyDayCount, "7"), isVisibleToClients: true),
                new SettingDefinition(AppSettings.HostManagement.BillingLegalName, GetFromAppSettings(AppSettings.HostManagement.BillingLegalName, "")),
                new SettingDefinition(AppSettings.HostManagement.BillingAddress, GetFromAppSettings(AppSettings.HostManagement.BillingAddress, "")),
                new SettingDefinition(AppSettings.Recaptcha.SiteKey, GetFromSettings("Recaptcha:SiteKey"), isVisibleToClients: true),
                new SettingDefinition(AppSettings.UiManagement.Theme, GetFromAppSettings(AppSettings.UiManagement.Theme, "default"), isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(AppSettings.UserManagement.AllowOneConcurrentLoginPerUser, GetFromAppSettings(AppSettings.UserManagement.AllowOneConcurrentLoginPerUser, "false"), isVisibleToClients: true, scopes: SettingScopes.Application | SettingScopes.Tenant)
            };
        }

        private IEnumerable<SettingDefinition> GetTenantSettings()
        {
            return new[]
            {
                new SettingDefinition(AppSettings.UserManagement.AllowSelfRegistration, GetFromAppSettings(AppSettings.UserManagement.AllowSelfRegistration, "true"), scopes: SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault, GetFromAppSettings(AppSettings.UserManagement.IsNewRegisteredUserActiveByDefault, "false"), scopes: SettingScopes.Tenant),
                new SettingDefinition(AppSettings.UserManagement.UseCaptchaOnRegistration, GetFromAppSettings(AppSettings.UserManagement.UseCaptchaOnRegistration, "true"), scopes: SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(AppSettings.Email.UseHostDefaultEmailSettings, GetFromAppSettings(AppSettings.Email.UseHostDefaultEmailSettings, GraceConsts.MultiTenancyEnabled? "true":"false"), scopes: SettingScopes.Tenant)
            };
        }

        private IEnumerable<SettingDefinition> GetSharedSettings()
        {
            return new[]
            {
                new SettingDefinition(AppSettings.UserManagement.TwoFactorLogin.IsGoogleAuthenticatorEnabled, GetFromAppSettings(AppSettings.UserManagement.TwoFactorLogin.IsGoogleAuthenticatorEnabled, "false"), scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(AppSettings.UserManagement.IsCookieConsentEnabled, GetFromAppSettings(AppSettings.UserManagement.IsCookieConsentEnabled, "false"), scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true),
                new SettingDefinition(AppSettings.UserManagement.IsQuickThemeSelectEnabled, GetFromAppSettings(AppSettings.UserManagement.IsQuickThemeSelectEnabled, "false"), scopes: SettingScopes.Application | SettingScopes.Tenant, isVisibleToClients: true)
            };
        }

        private string GetFromAppSettings(string name, string defaultValue = null)
        {
            return GetFromSettings("App:" + name, defaultValue);
        }

        private string GetFromSettings(string name, string defaultValue = null)
        {
            return _appConfiguration[name] ?? defaultValue;
        }

        private IEnumerable<SettingDefinition> GetDefaultThemeSettings()
        {
            var themeName = "default";

            return new[]
            {
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.DesktopFixedHeader, GetFromAppSettings(themeName + "." +AppSettings.UiManagement.Header.DesktopFixedHeader, "true"),isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.MobileFixedHeader, GetFromAppSettings(themeName + "." +AppSettings.UiManagement.Header.MobileFixedHeader, "false"),isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Header.Skin, GetFromAppSettings(themeName + "." +AppSettings.UiManagement.Header.Skin, "light"),isVisibleToClients: true, scopes: SettingScopes.All),

                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SubHeader.Fixed, GetFromAppSettings(themeName + "." +AppSettings.UiManagement.SubHeader.Fixed, "true"),isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.SubHeader.Style, GetFromAppSettings(themeName + "." +AppSettings.UiManagement.SubHeader.Style, "solid"),isVisibleToClients: true, scopes: SettingScopes.All),

                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LeftAside.AsideSkin, GetFromAppSettings(themeName + "." +AppSettings.UiManagement.LeftAside.AsideSkin, "light"), isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LeftAside.FixedAside, GetFromAppSettings(themeName + "." +AppSettings.UiManagement.LeftAside.FixedAside, "true"),isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LeftAside.AllowAsideMinimizing, GetFromAppSettings(themeName + "." +AppSettings.UiManagement.LeftAside.AllowAsideMinimizing, "true"),isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LeftAside.DefaultMinimizedAside, GetFromAppSettings(themeName + "." +AppSettings.UiManagement.LeftAside.DefaultMinimizedAside, "false"),isVisibleToClients: true, scopes: SettingScopes.All),
                new SettingDefinition(themeName + "." + AppSettings.UiManagement.LeftAside.SubmenuToggle, GetFromAppSettings(themeName + "." +AppSettings.UiManagement.LeftAside.SubmenuToggle, "false"),isVisibleToClients: true, scopes: SettingScopes.All),

                new SettingDefinition(themeName + "." + AppSettings.UiManagement.Footer.FixedFooter, GetFromAppSettings(themeName + "." +AppSettings.UiManagement.Footer.FixedFooter, "false"),isVisibleToClients: true, scopes: SettingScopes.All)
            };
        }
    
    }
}
