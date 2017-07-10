using System;
using Gscienty.WechatPay.Attributes;
using Gscienty.WechatPay.Defines;

namespace Gscienty.WechatPay.Entities
{
    public sealed class RequestUnifiedOrderEntity : WechatBaseEntity
    {
        [IsRequisiteParameter]
        [ParameterName("body")]
        [ParameterMaxLength(128)]
        public string Body { get; set; }

        [ParameterName("detail")]
        [ParameterMaxLength(6000)]
        public string Detail { get; set; }

        [ParameterName("attach")]
        [ParameterMaxLength(127)]
        public string Attach { get; set; }

        [IsRequisiteParameter]
        [ParameterName("out_trade_no")]
        [ParameterMaxLength(32)]
        public string OutTradeNumber { get; set; }

        [ParameterName("fee_type")]
        [ParameterMaxLength(16)]
        public FeeType FeeType { get; set; }

        [IsRequisiteParameter]
        [ParameterName("total_fee")]
        public int TotalFee { get; set; }

        [IsRequisiteParameter]
        [ParameterName("spbill_create_ip")]
        [ParameterMaxLength(16)]
        public string SpbillCreateIp { get; set; }

        [ParameterName("time_start")]
        [ParameterMaxLength(14)]
        public DateTime TimeStart { get; set; }

        [ParameterName("time_expire")]
        [ParameterMaxLength(14)]
        public DateTime TimeExpire { get; set; }

        [ParameterName("goods_tag")]
        [ParameterMaxLength(32)]
        public string GoodsTag { get; set; }

        [IsRequisiteParameter]
        [ParameterName("notify_url")]
        [ParameterMaxLength(256)]
        public string NotifyURI { get; set; }

        [IsRequisiteParameter]
        [ParameterName("trade_type")]
        [ParameterMaxLength(16)]
        public TradeType TradeType { get; set; }

        [ParameterName("product_id")]
        [ParameterMaxLength(32)]
        public string ProductId { get; set; }

        [ParameterName("limit_pay")]
        [ParameterMaxLength(32)]
        public string LimitPay { get; set; }

        [ParameterName("openid")]
        [ParameterMaxLength(128)]
        public string OpenID { get; set; }

        [ParameterName("scene_info")]
        [ParameterMaxLength(256)]
        public string SceneInfo { get; set; }
    }
}