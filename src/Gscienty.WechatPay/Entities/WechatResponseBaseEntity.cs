using Gscienty.WechatPay.Attributes;
using Gscienty.WechatPay.Defines;

namespace Gscienty.WechatPay.Entities
{
    public abstract class WechatResponseBaseEntity : WechatBaseEntity
    {
        [IsRequisiteParameter]
        [ParameterName("return_code")]
        [ParameterMaxLength(16)]
        public ReturnCodeType ReturnCode { get; set; }
        
        [ParameterName("return_msg")]
        [ParameterMaxLength(128)]
        public string ReturnMessage { get; set; }

        [IsRequisiteParameter]
        [ParameterName("result_code")]
        [ParameterMaxLength(16)]
        public ReturnCodeType ResultCode { get; set; }

        [ParameterName("err_code")]
        [ParameterMaxLength(32)]
        public ErrorCodeType ErrorCode { get; set; }

        [ParameterName("err_code_des")]
        [ParameterMaxLength(128)]
        public string ErrorCodeDescription { get; set; }
    }
}