using System;
using Demeter.PayComponent.Wechat.RequestEntity.Attribute;

namespace Demeter.PayComponent.Wechat.RequestEntity
{
    public sealed class UnifiedOrderRequest
    {
        [RequestName("body")]
        public string Body { get; set; }
        [RequestName("detail")]
        public string Detail { get; set; }
        [RequestName("device_info")]
        public string DeviceInfomation { get; set; }
        [RequestName("attach")]
        public string Attach { get; set; }
        [RequestName("out_trade_no")]
        public string OutTradeNumber { get; set; }
        [RequestName("fee_type")]
        public string FeeType { get; set; }
        [RequestName("total_fee")]
        public int? TotalFee { get; set; }
        [RequestName("spbill_create_ip")]
        public string SpbillCreateIp { get; set; }
        [RequestName("time_start")]
        public string TimeStart { get; set; }
        [RequestName("time_expire")]
        public string TimeExpire { get; set; }
        [RequestName("goods_tag")]
        public string GoodsTag { get; set; }
        [RequestName("notify_url")]
        public string NotifyURI { get; set; }
        [RequestName("trade_type")]
        public string TradeType { get; set; }
        [RequestName("product_id")]
        public string ProductId { get; set; }
        [RequestName("limit_pay")]
        public string LimitPay { get; set; }
        [RequestName("openid")]
        public string OpenID { get; set; }
        [RequestName("scene_info")]
        public string SceneInfo { get; set; }
    }
}