using System;
using System.Threading.Tasks;
using Abp;
using Abp.AspNetCore.SignalR.Hubs;
using Abp.Auditing;
using Abp.Localization;
using Abp.RealTime;
using Abp.Runtime.Session;
using Abp.UI;
using Castle.Core.Logging;
using Castle.Windsor;
using TuDou.Grace.Chat;

namespace TuDou.Grace.Web.Chat.SignalR
{
    public class ChatHub : OnlineClientHubBase
    {
        private readonly IChatMessageManager _chatMessageManager;
        private readonly ILocalizationManager _localizationManager;
        private readonly IWindsorContainer _windsorContainer;
        private bool _isCallByRelease;

        /// <summary>
        /// 初始化<see cref="ChatHub"/>类的新实例。
        /// </summary>
        public ChatHub(
            IChatMessageManager chatMessageManager,
            ILocalizationManager localizationManager,
            IWindsorContainer windsorContainer,
            IOnlineClientManager<ChatChannel> onlineClientManager,
            IClientInfoProvider clientInfoProvider) : base(onlineClientManager, clientInfoProvider)
        {
            _chatMessageManager = chatMessageManager;
            _localizationManager = localizationManager;
            _windsorContainer = windsorContainer;

            Logger = NullLogger.Instance;
            AbpSession = NullAbpSession.Instance;
        }

        public async Task<string> SendMessage(SendChatMessageInput input)
        {
            var sender = AbpSession.ToUserIdentifier();
            var receiver = new UserIdentifier(input.TenantId, input.UserId);

            try
            {
                await _chatMessageManager.SendMessageAsync(sender, receiver, input.Message, input.TenancyName, input.UserName, input.ProfilePictureId);
                return string.Empty;
            }
            catch (UserFriendlyException ex)
            {
                Logger.Warn("无法向用户发送聊天消息:" + receiver);
                Logger.Warn(ex.ToString(), ex);
                return ex.Message;
            }
            catch (Exception ex)
            {
                Logger.Warn("无法向用户发送聊天消息:" + receiver);
                Logger.Warn(ex.ToString(), ex);
                return _localizationManager.GetSource("AbpWeb").GetString("InternalServerError");
            }
        }

        public void Register()
        {
            Logger.Debug("一个客户端已连接" + Context.ConnectionId);
        }

        protected override void Dispose(bool disposing)
        {
            if (_isCallByRelease)
            {
                return;
            }
            base.Dispose(disposing);
            if (disposing)
            {
                _isCallByRelease = true;
                _windsorContainer.Release(this);
            }
        }
    }
}