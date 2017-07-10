using Gscienty.WechatPay.Attributes;

namespace Gscienty.WechatPay.Defines
{
    public enum ReturnCodeType
    {
        [DefineMapName("SUCCESS")]
        Success,
        [DefineMapName("FAIL")]
        Failure
    }
}