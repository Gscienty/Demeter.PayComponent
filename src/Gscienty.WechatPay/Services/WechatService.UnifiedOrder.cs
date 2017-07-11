using Gscienty.WechatPay.Entities;

namespace Gscienty.WechatPay.Services
{
    partial class WechatService
    {
        ResponseUnifiedOrderEntity UnifiedOrder(RequestUnifiedOrderEntity entity)
        {
            entity.NotifyURI = this._settings.PaymentNotifyURI;

            this.PackageEntity(entity);
            string requestXML = XMLConstructor.Constructor(entity);

            return this.CallRemoteMethod<ResponseUnifiedOrderEntity>(
                "https://api.mch.weixin.qq.com/pay/unifiedorder",
                requestXML
            );
        }
    }
}