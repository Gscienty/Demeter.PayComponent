using Demeter.PayComponent.Wechat.RequestEntity.Attribute;
namespace Demeter.PayComponent.Wechat.RequestEntity
{
    public sealed class RefundRequest
    {
        [RequestName("transaction_id")]
        public string TransactionID { get; set; }

        [RequestName("out_trade_no")]
        public string OutTradeNumber { get; set; }

        [RequestName("out_refund_no")]
        public string OutRefundNumber { get; set; }

        [RequestName("total_fee")]
        public int? TotalFee { get; set; }

        [RequestName("refund_fee")]
        public int? RefundFee { get; set; }

        [RequestName("refund_fee_type")]
        public string RefundFeeType { get; set; }

        [RequestName("refund_desc")]
        public string RefundDescription { get; set; }

        [RequestName("refund_account")]
        public string RefundAccount { get; set; }
    }
}