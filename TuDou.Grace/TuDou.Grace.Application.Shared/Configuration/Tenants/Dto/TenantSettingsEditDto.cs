using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Abp.Runtime.Validation;
using Abp.Timing;
using TuDou.Grace.Configuration.Host.Dto;

namespace TuDou.Grace.Configuration.Tenants.Dto
{
    public class TenantSettingsEditDto
    {

        [Required]
        public TenantUserManagementSettingsEditDto UserManagement { get; set; }

        public TenantEmailSettingsEditDto Email { get; set; }

        public LdapSettingsEditDto Ldap { get; set; }

        [Required]
        public SecuritySettingsEditDto Security { get; set; }


        /// <summary>
        /// 这个验证针对的是单租户应用程序。
        /// 因为，这些设置只能由单租户应用程序中的租户设置。
        /// </summary>
        public void ValidateHostSettings()
        {
            var validationErrors = new List<ValidationResult>();

            if (Email == null)
            {
                validationErrors.Add(new ValidationResult("电子邮件设置不能为空", new[] { "Email" }));
            }

            if (validationErrors.Count > 0)
            {
                throw new AbpValidationException("方法参数无效!有关详细信息，请参见ValidationErrors.", validationErrors);
            }
        }
    }
}