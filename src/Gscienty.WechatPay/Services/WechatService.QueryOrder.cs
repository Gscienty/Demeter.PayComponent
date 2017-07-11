using Gscienty.WechatPay.Entities;

namespace Gscienty.WechatPay.Services
{
    partial class WechatService
    {
        ResponseOrderQueryEntity QueryOrder(RequestOrderQueryEntity entity)
        {
            this.PackageEntity(entity);

            return this.CallRemoteMethod<ResponseOrderQueryEntity>(
                "https://api.mch.weixin.qq.com/pay/orderquery",
                XMLConstructor.Constructor(entity)
            );
        }
    }
}