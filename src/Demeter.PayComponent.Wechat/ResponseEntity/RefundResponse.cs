using Demeter.PayComponent.Wechat.ResponseEntity.Attribute;

namespace Demeter.PayComponent.Wechat.ResponseEntity
{
    public sealed class RefundResponse : ResponseBase
    {
        [ResponseName("transaction_id")]
        public string TransactionID { get; set; }

        [ResponseName("out_trade_no")]
        public string OutTradeNumber { get; set; }

        [ResponseName("out_refund_no")]
        public string OutRefundNumber { get; set; }

        [ResponseName("refund_id")]
        public string RefundID { get; set; }

        [ResponseName("refund_fee")]
        public int? RefundFee { get; set; }

        [ResponseName("settlement_refund_fee")]
        public int? SettlementRefundFee { get; set; }

        [ResponseName("total_fee")]
        public int? TotalFee { get; set; }

        [ResponseName("settlement_total_fee")]
        public int? SettlementTotalFee { get; set; }

        [ResponseName("fee_type")]
        public string FeeType { get; set; }

        [ResponseName("cash_fee")]
        public int? CashFee { get; set; }

        [ResponseName("cash_fee_type")]
        public string CashFeeType { get; set; }

        [ResponseName("cash_refund_fee")]
        public int? CashRefundFee { get; set; }

        [ResponseName("coupon_count")]
        public int? CouponCount { get; set; }

        [ResponseName("coupon_type_$n")]
        public string CouponType { get; set; }

        [ResponseName("coupon_id_$n")]
        public string CouponId { get; set; }

        [ResponseName("coupon_fee_$n")]
        public int? CouponFee { get; set; }
    }
}