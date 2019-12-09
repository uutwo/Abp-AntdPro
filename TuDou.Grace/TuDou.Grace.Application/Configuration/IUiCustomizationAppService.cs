using Abp.Application.Services;
using System.Collections.Generic;
using System.Threading.Tasks;
using TuDou.Grace.Configuration.Dto;

namespace TuDou.Grace.Configuration
{
    public interface IUiCustomizationSettingsAppService : IApplicationService
    {
        Task<List<ThemeSettingsDto>> GetUiManagementSettings();

        Task UpdateUiManagementSettings(ThemeSettingsDto settings);

        Task UpdateDefaultUiManagementSettings(ThemeSettingsDto settings);

        Task UseSystemDefaultSettings();
    }
}
