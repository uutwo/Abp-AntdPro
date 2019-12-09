using System.Threading.Tasks;

namespace TuDou.Grace.Security.Recaptcha
{
    public interface IRecaptchaValidator
    {
        Task ValidateAsync(string captchaResponse);
    }
}
