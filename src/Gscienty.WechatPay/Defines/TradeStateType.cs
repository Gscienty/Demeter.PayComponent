using Gscienty.WechatPay.Attributes;

namespace Gscienty.WechatPay.Defines
{
    public enum TradeStateType
    {
        [DefineMapName("SUCCESS")]
        Success,
        [DefineMapName("REFUND")]
        Refund,
        [DefineMapName("NOTPAY")]
        NotPay,
        [DefineMapName("CLOSED")]
        Closed,
        [DefineMapName("REVOKED")]
        Revoked,
        [DefineMapName("USERPAYING")]
        UserPaying,
        [DefineMapName("PAYERROR")]
        PayError
    }
}