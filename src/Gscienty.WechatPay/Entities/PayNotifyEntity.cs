using System;
using Gscienty.WechatPay.Attributes;
using Gscienty.WechatPay.Defines;

namespace Gscienty.WechatPay.Entities
{
    public sealed class PayNotifyEntity : WechatResponseBaseEntity
    {
        [IsRequisiteParameter]
        [ParameterName("openid")]
        [ParameterMaxLength(32)]
        public string OpenId { get; set; }
        
        [ParameterName("is_subscribe")]
        [ParameterMaxLength(1)]
        public string IsSubscribe { get; set; }

        [IsRequisiteParameter]
        [ParameterName("trade_type")]
        [ParameterMaxLength(16)]
        public TradeType TradeType { get; set; }

        [IsRequisiteParameter]
        [ParameterName("bank_type")]
        [ParameterMaxLength(16)]
        public string BankType { get; set; }

        [IsRequisiteParameter]
        [ParameterName("total_fee")]
        public int? TotalFee { get; set; }

        [ParameterName("settlement_total_fee")]
        public int? SettlementTotalFee { get; set; }

        [ParameterName("fee_type")]
        [ParameterMaxLength(8)]
        public CashFeeType FeeType { get; set; }

        [IsRequisiteParameter]
        [ParameterName("cash_fee")]
        public int? CashFee { get; set; }

        [ParameterName("cash_fee_type")]
        [ParameterMaxLength(16)]
        public CashFeeType CashFeeType { get; set; }

        [ParameterName("coupon_count")]
        public int? CouponCount { get; set; }

        [ParameterName("coupon_type_$n")]
        public CouponType CouponType { get; set; }

        [ParameterName("coupon_id_$n")]
        [ParameterMaxLength(20)]
        public string CouponId { get; set; }

        [ParameterName("coupon_fee_$n")]
        public int? CouponFee { get; set; }

        [IsRequisiteParameter]
        [ParameterName("transaction_id")]
        [ParameterMaxLength(32)]
        public string TransactionId { get; set; }

        [IsRequisiteParameter]
        [ParameterName("out_trade_no")]
        [ParameterMaxLength(32)]
        public string OutTradeNumber { get; set; }

        [ParameterName("attach")]
        [ParameterMaxLength(128)]
        public string Attach { get; set; }

        [ParameterName("time_end")]
        [ParameterMaxLength(14)]
        public DateTime? TimeEnd { get; set; }
    }
}