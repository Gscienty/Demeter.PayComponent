using Gscienty.WechatPay.Entities;

namespace Gscienty.WechatPay.Services
{
    partial class WechatService
    {
        public ResponseRefundEntity Refund(RequestRefundEntity entity)
        {
            this.PackageEntity(entity);

            return this.CallRemoteMethodSecurity<ResponseRefundEntity>(
                "https://api.mch.weixin.qq.com/secapi/pay/refund",
                XMLConstructor.Constructor(entity)
            );
        }
    }
}