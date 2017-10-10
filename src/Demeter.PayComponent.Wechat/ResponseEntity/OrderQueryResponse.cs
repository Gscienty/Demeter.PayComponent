using Demeter.PayComponent.Wechat.ResponseEntity.Attribute;

namespace Demeter.PayComponent.Wechat.ResponseEntity
{
    public sealed class OrderQueryResponse : ResponseBase
    {
        [ResponseName("openid")]
        public string OpenId { get; set; }
        
        [ResponseName("is_subscribe")]
        public string IsSubscribe { get; set; }

        [ResponseName("trade_type")]
        public string TradeType { get; set; }

        [ResponseName("trade_state")]
        public string TradeState { get; set; }

        [ResponseName("bank_type")]
        public string BankType { get; set; }

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

        [ResponseName("coupon_count")]
        public int? CouponCount { get; set; }

        [ResponseName("coupon_type_$n")]
        public string CouponType { get; set; }

        [ResponseName("coupon_id_$n")]
        public string CouponId { get; set; }

        [ResponseName("coupon_fee_$n")]
        public int? CouponFee { get; set; }

        [ResponseName("transaction_id")]
        public string TransactionId { get; set; }

        [ResponseName("out_trade_no")]
        public string OutTradeNumber { get; set; }

        [ResponseName("attach")]
        public string Attach { get; set; }

        [ResponseName("time_end")]
        public string TimeEnd { get; set; }

        [ResponseName("trade_state_desc")]
        public string TradeStateDescription { get; set; }
    }
}