using System;
using Gscienty.WechatPay.Attributes;
using Gscienty.WechatPay.Defines;

namespace Gscienty.WechatPay.Entities
{
    public abstract class WechatBaseEntity
    {
        [IsRequisiteParameter]
        [ParameterName("appid")]
        [ParameterMaxLength(32)]
        public string AppId { get; set; }

        [IsRequisiteParameter]
        [ParameterName("mch_id")]
        [ParameterMaxLength(32)]
        public string MerchantId { get; set; }

        [ParameterName("device_info")]
        [ParameterMaxLength(32)]
        public string DeviceInfo { get; set; }

        [IsRequisiteParameter]
        [ParameterName("nonce_str")]
        [ParameterMaxLength(32)]
        public string Nonce { get; set; }

        [IsRequisiteParameter]
        [ParameterName("sign")]
        [ParameterMaxLength(32)]
        public string Sign { get; set; }

        [ParameterName("sign_type")]
        [ParameterMaxLength(32)]
        public SignType SignType { get; set; }
    }
}