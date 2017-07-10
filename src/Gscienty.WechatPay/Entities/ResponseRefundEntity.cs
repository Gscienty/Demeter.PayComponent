using Gscienty.WechatPay.Attributes;
using Gscienty.WechatPay.Defines;

namespace Gscienty.WechatPay.Entities
{
    public sealed class ResponseRefundEntity : WechatResponseBaseEntity
    {
        [IsRequisiteParameter]
        [ParameterName("transaction_id")]
        [ParameterMaxLength(28)]
        public string TransactionID { get; set; }

        [IsRequisiteParameter]
        [ParameterName("out_trade_no")]
        [ParameterMaxLength(32)]
        public string OutTradeNumber { get; set; }

        [IsRequisiteParameter]
        [ParameterName("out_refund_no")]
        [ParameterMaxLength(64)]
        public string OutRefundNumber { get; set; }

        [IsRequisiteParameter]
        [ParameterName("refund_id")]
        [ParameterMaxLength(32)]
        public string RefundID { get; set; }

        [IsRequisiteParameter]
        [ParameterName("refund_fee")]
        public int? RefundFee { get; set; }

        [ParameterName("settlement_refund_fee")]
        public int? SettlementRefundFee { get; set; }

        [IsRequisiteParameter]
        [ParameterName("total_fee")]
        public int? TotalFee { get; set; }

        [ParameterName("settlement_total_fee")]
        public int? SettlementTotalFee { get; set; }

        [ParameterName("fee_type")]
        [ParameterMaxLength(8)]
        public CashFeeType? FeeType { get; set; }

        [IsRequisiteParameter]
        [ParameterName("cash_fee")]
        public int? CashFee { get; set; }

        [ParameterName("cash_fee_type")]
        [ParameterMaxLength(16)]
        public CashFeeType? CashFeeType { get; set; }

        [ParameterName("cash_refund_fee")]
        public int? CashRefundFee { get; set; }

        [ParameterName("coupon_count")]
        public int? CouponCount { get; set; }

        [ParameterName("coupon_type_$n")]
        public CouponType? CouponType { get; set; }

        [ParameterName("coupon_id_$n")]
        [ParameterMaxLength(20)]
        public string CouponId { get; set; }

        [ParameterName("coupon_fee_$n")]
        public int? CouponFee { get; set; }
    }
}