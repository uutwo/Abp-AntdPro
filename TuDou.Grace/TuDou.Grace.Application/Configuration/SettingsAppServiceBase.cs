using Abp.Net.Mail;
using System.Threading.Tasks;
using TuDou.Grace.Configuration.Host.Dto;

namespace TuDou.Grace.Configuration
{
    public abstract class SettingsAppServiceBase : GraceAppServiceBase
    {
        private readonly IEmailSender _emailSender;

        protected SettingsAppServiceBase(
            IEmailSender emailSender)
        {
            _emailSender = emailSender;
        }

        #region 发送测试邮件

        public async Task SendTestEmail(SendTestEmailInput input)
        {
            await _emailSender.SendAsync(
                input.EmailAddress,
                L("TestEmail_Subject"),
                L("TestEmail_Body")
            );
        }

        #endregion
    }
}
