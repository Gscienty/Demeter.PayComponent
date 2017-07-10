using Gscienty.WechatPay.Attributes;

namespace Gscienty.WechatPay.Defines
{
    public enum ReturnCodeType
    {
        Unset,
        [DefineMapName("SUCCESS")]
        Success,
        [DefineMapName("FAIL")]
        Failure
    }
}