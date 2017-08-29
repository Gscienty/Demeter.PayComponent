using System;
using Gscienty.WechatPay.Attributes;

namespace Gscienty.WechatPay.Entities
{
    public sealed class PrePayIdEntity : WechatBaseEntity
    {
        [ParameterName("appId")]
        public string PrePayAppId { get; set; }

        [ParameterName("timeStamp")]
        public string Timestamp { get; set; }

        [ParameterName("package")]
        public string Package { get; set; }

        [ParameterName("nonceStr")]
        public string PrePayNonce { get; set; }

        [ParameterName("signType")]
        public string PrePaySignType { get; set; }

        [ParameterName("paySign")]
        public string PrePaySign { get; set; }
    }
}