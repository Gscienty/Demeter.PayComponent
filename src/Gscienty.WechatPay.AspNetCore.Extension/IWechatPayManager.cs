using Gscienty.WechatPay.Entities;

namespace Gscienty.WechatPay.AspNetCore.Extension
{
    public interface IWechatPayManager
    {
        ResponseUnifiedOrderEntity UnifiedOrder(RequestUnifiedOrderEntity entity);
        ResponseRefundEntity Refund(RequestRefundEntity entity);
        ResponseOrderQueryEntity QueryOrder(RequestOrderQueryEntity entity);
        PayNotifyEntity PayNotify(string responseString);
        ResponseCloseOrderEntity CloseOrder(RequestCloseOrderEntity entity);
        WechatAccessToken GetAccessToken(string code);
    }
}