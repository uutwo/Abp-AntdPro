using System.Collections.Generic;
using System.Linq;
using Abp.Localization;
using Microsoft.EntityFrameworkCore;
using TuDou.Grace.EntityFrameworkCore;

namespace TuDou.Grace.Migrations.Seed.Host
{
    public class DefaultLanguagesCreator
    {
        public static List<ApplicationLanguage> InitialLanguages => GetInitialLanguages();

        private readonly GraceDbContext _context;

        private static List<ApplicationLanguage> GetInitialLanguages()
        {
            var tenantId = GraceConsts.MultiTenancyEnabled ? null : (int?)1;
            return new List<ApplicationLanguage>
            {
                new ApplicationLanguage(tenantId, "en", "English", "icon-yingguo"),
                new ApplicationLanguage(tenantId, "ar", "العربية", "icon-icon_AEalabolianheqiuchangguo"),
                new ApplicationLanguage(tenantId, "de", "Deutsch", "icon-deguo"),
                new ApplicationLanguage(tenantId, "it", "Italiano", "icon-yidali"),
                new ApplicationLanguage(tenantId, "fr", "Français", "icon-faguo"),
                new ApplicationLanguage(tenantId, "pt-BR", "Português (Brasil)", "icon-baxi"),
                new ApplicationLanguage(tenantId, "tr", "Türkçe", "icon-tuerqi"),
                new ApplicationLanguage(tenantId, "ru", "Pусский", "icon-eluosi"),
                new ApplicationLanguage(tenantId, "zh-Hans", "简体中文", "icon-zhongguo"),
                new ApplicationLanguage(tenantId, "es-MX", "Español (México)", "icon-moxige"),
                new ApplicationLanguage(tenantId, "es", "Español (Spanish)", "icon-xibanya"),
                new ApplicationLanguage(tenantId, "vi", "Tiếng Việt", "icon-yuenan"),
                new ApplicationLanguage(tenantId, "nl", "Dutch (Nederlands)", "icon-helan")
            };
        }

        public DefaultLanguagesCreator(GraceDbContext context)
        {
            _context = context;
        }

        public void Create()
        {
            CreateLanguages();
        }

        private void CreateLanguages()
        {
            foreach (var language in InitialLanguages)
            {
                AddLanguageIfNotExists(language);
            }
        }

        private void AddLanguageIfNotExists(ApplicationLanguage language)
        {
            if (_context.Languages.IgnoreQueryFilters().Any(l => l.TenantId == language.TenantId && l.Name == language.Name))
            {
                return;
            }

            _context.Languages.Add(language);

            _context.SaveChanges();
        }
    }
}