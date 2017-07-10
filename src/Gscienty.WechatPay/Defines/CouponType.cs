using Gscienty.WechatPay.Attributes;
namespace Gscienty.WechatPay.Defines
{
    public enum CouponType
    {
        Unset,
        [DefineMapName("CASH")]
        Cash,
        [DefineMapName("NO_CASH")]
        NoCash
    }
}