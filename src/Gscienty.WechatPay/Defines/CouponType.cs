using Gscienty.WechatPay.Attributes;
namespace Gscienty.WechatPay.Defines
{
    public enum CouponType
    {
        [DefineMapName("CASH")]
        Cash,
        [DefineMapName("NO_CASH")]
        NoCash
    }
}