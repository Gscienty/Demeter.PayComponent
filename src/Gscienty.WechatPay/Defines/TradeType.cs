using Gscienty.WechatPay.Attributes;

namespace Gscienty.WechatPay.Defines
{
    public enum TradeType
    {
        Unset,
        [DefineMapName("JSAPI")]
        JSAPI,
        [DefineMapName("NATIVE")]
        Native,
        [DefineMapName("APP")]
        APP,
        [DefineMapName("MICROPAY")]
        MicroPay
    }
}