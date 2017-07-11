using Gscienty.WechatPay.Attributes;

namespace Gscienty.WechatPay.Entities
{
    public sealed class RequestOrderQueryEntity : WechatBaseEntity
    {
        [ParameterName("transaction_id")]
        [ParameterMaxLength(32)]
        public string TransactionID { get; set; }

        [ParameterName("out_trade_no")]
        [ParameterMaxLength(32)]
        public string OutTradeNumber { get; set; }
    }
}