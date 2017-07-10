using Gscienty.WechatPay.Attributes;
using Gscienty.WechatPay.Defines;

namespace Gscienty.WechatPay.Entities
{
    public sealed class ResponseUnifiedOrderEntity : WechatResponseBaseEntity
    {
        [IsRequisiteParameter]
        [ParameterName("trade_type")]
        [ParameterMaxLength(16)]
        public TradeType? TradeType { get; set; }

        [IsRequisiteParameter]
        [ParameterName("prepay_id")]
        [ParameterMaxLength(64)]
        public string PrePayId { get; set; }

        [ParameterName("code_url")]
        [ParameterMaxLength(64)]
        public string CodeURI { get; set; }
    }
}
