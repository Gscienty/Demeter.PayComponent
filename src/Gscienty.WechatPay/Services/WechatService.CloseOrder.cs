using Gscienty.WechatPay.Entities;

namespace Gscienty.WechatPay.Services
{
    partial class WechatService
    {
        public ResponseCloseOrderEntity CloseOrder(RequestCloseOrderEntity entity)
        {
            this.PackageEntity(entity);

            return this.CallRemoteMethod<ResponseCloseOrderEntity>(
                "https://api.mch.weixin.qq.com/pay/closeorder",
                XMLConstructor.Constructor(entity)
            );
        }
    }
}