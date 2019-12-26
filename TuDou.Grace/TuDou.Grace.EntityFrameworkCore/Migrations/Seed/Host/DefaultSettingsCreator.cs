using System.Linq;
using Abp.Configuration;
using Abp.Localization;
using Abp.MultiTenancy;
using Abp.Net.Mail;
using Microsoft.EntityFrameworkCore;
using TuDou.Grace.EntityFrameworkCore;

namespace TuDou.Grace.Migrations.Seed.Host
{
    public class DefaultSettingsCreator
    {
        private readonly GraceDbContext _context;

        public DefaultSettingsCreator(GraceDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            int? tenantId = null;

            if (GraceConsts.MultiTenancyEnabled == false)
            {
                tenantId = MultiTenancyConsts.DefaultTenantId;
            }

            //邮箱
            AddSettingIfNotExists(EmailSettingNames.DefaultFromAddress, "graceabp@163.com", tenantId);
            AddSettingIfNotExists(EmailSettingNames.DefaultFromDisplayName, "tudou", tenantId);
            AddSettingIfNotExists(EmailSettingNames.Smtp.Host, "smtp.163.com", tenantId);
            AddSettingIfNotExists(EmailSettingNames.Smtp.Port, "587", tenantId);
            AddSettingIfNotExists(EmailSettingNames.Smtp.UserName, "graceabp@163.com", tenantId);
            AddSettingIfNotExists(EmailSettingNames.Smtp.Password, "123qwe", tenantId);
            AddSettingIfNotExists(EmailSettingNames.Smtp.UseDefaultCredentials, "false", tenantId);
            AddSettingIfNotExists(EmailSettingNames.Smtp.Domain, "", tenantId);
            AddSettingIfNotExists(EmailSettingNames.Smtp.EnableSsl, "true", tenantId);
            //多语言
            AddSettingIfNotExists(LocalizationSettingNames.DefaultLanguage, "zh-Hans", tenantId);
        }

        private void AddSettingIfNotExists(string name, string value, int? tenantId = null)
        {
            if (_context.Settings.IgnoreQueryFilters().Any(s => s.Name == name && s.TenantId == tenantId && s.UserId == null))
            {
                return;
            }

            _context.Settings.Add(new Setting(tenantId, null, name, value));
            _context.SaveChanges();
        }
    }
}