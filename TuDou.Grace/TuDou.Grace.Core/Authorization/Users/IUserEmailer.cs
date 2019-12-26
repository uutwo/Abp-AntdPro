using System.Threading.Tasks;
using TuDou.Grace.Chat;

namespace TuDou.Grace.Authorization.Users
{
    public interface IUserEmailer
    {
        /// <summary>
        /// 发送电子邮件激活链接到用户的电子邮件地址。
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="link">激活链接</param>
        /// <param name="plainPassword">
        /// 可以设置为用户的普通密码，以包含在电子邮件中。
        /// </param>
        Task SendEmailActivationLinkAsync(User user, string link, string plainPassword = null);

        /// <summary>
        /// 发送重置密码链接到用户的电子邮件地址。
        /// </summary>
        /// <param name="user">用户</param>
        /// <param name="link">重置密码链接</param>
        Task SendPasswordResetLinkAsync(User user, string link = null);

        /// <summary>
        /// 向用户的电子邮件发送未读的聊天消息。
        /// </summary>
        /// <param name="user"></param>
        /// <param name="senderUsername"></param>
        /// <param name="senderTenancyName"></param>
        /// <param name="chatMessage"></param>
        Task TryToSendChatMessageMail(User user, string senderUsername, string senderTenancyName, ChatMessage chatMessage);
    }
}
