using Gscienty.WechatPay.Attributes;

namespace Gscienty.WechatPay.Entities
{
    public sealed class RequestCloseOrderEntity : WechatBaseEntity
    {
        [IsRequisiteParameter]
        [ParameterName("out_trade_no")]
        [ParameterMaxLength(32)]
        public string OutTradeNumber { get; set; }
    }
}