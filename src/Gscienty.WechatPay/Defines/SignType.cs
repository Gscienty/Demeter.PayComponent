using Gscienty.WechatPay.Attributes;

namespace Gscienty.WechatPay.Defines
{
    public enum SignType
    {
        Unset,
        [DefineMapName("MD5")]
        MD5,
        [DefineMapName("HMAC-SHA256")]
        HMACSHA256
    }
}