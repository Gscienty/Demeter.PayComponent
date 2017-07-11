using Gscienty.WechatPay.Entities;

namespace Gscienty.WechatPay.Services
{
    public sealed partial class WechatService
    {
        private WechatSettings _settings;

        public WechatService(WechatSettings settings)
        {
            this._settings = settings;
        }

    }
}