using Gscienty.WechatPay.Entities;

namespace Gscienty.WechatPay.Services
{
    partial class WechatService
    {
        public PayNotifyEntity PayNotify(string responseString)
        {
            PayNotifyEntity notifyEntity = XMLParser.Parser<PayNotifyEntity>(responseString);
            string sign = SignBuilder.GenerateSign(notifyEntity, this._settings.SecurityKey);

            if (notifyEntity.Sign.Equals(sign))
            {
                return notifyEntity;
            }
            else
            {
                return null;
            }
        }
    }
}