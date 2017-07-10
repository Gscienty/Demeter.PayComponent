using Gscienty.WechatPay.Attributes;
using Gscienty.WechatPay.Defines;

namespace Gscienty.WechatPay.Entities
{
    [IsRequisiteCertificate]
    public sealed class RequestRefundEntity : WechatBaseEntity
    {
        [ParameterName("transaction_id")]
        [ParameterMaxLength(32)]
        public string TransactionID { get; set; }

        [ParameterName("out_trade_no")]
        [ParameterMaxLength(32)]
        public string OutTradeNumber { get; set; }

        [IsRequisiteParameter]
        [ParameterName("out_refund_no")]
        [ParameterMaxLength(64)]
        public string OutRefundNumber { get; set; }

        [IsRequisiteParameter]
        [ParameterName("total_fee")]
        public int? TotalFee { get; set; }

        [IsRequisiteParameter]
        [ParameterName("refund_fee")]
        public int? RefundFee { get; set; }

        [ParameterName("refund_fee_type")]
        [ParameterMaxLength(8)]
        public CashFeeType RefundFeeType { get; set; }

        [ParameterName("refund_desc")]
        [ParameterMaxLength(80)]
        public string RefundDescription { get; set; }

        [ParameterName("refund_account")]
        [ParameterMaxLength(30)]
        public string RefundAccount { get; set; }
    }
}