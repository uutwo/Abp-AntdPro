using Abp.Authorization.Users;
using System.ComponentModel.DataAnnotations;

namespace TuDou.Grace.Authorization.Accounts.Dto
{
    public class SendPasswordResetCodeInput
    {
        [Required]
        [MaxLength(AbpUserBase.MaxEmailAddressLength)]
        public string EmailAddress { get; set; }
    }
}