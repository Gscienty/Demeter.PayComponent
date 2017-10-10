using System;
namespace Demeter.PayComponent.Wechat
{
    internal static class Constant
    {
        public const string UnifiedOrder = "https://api.mch.weixin.qq.com/pay/unifiedorder";

        public const string OrderQuery = "https://api.mch.weixin.qq.com/pay/orderquery";

        public const string CloseOrder = "https://api.mch.weixin.qq.com/pay/closeorder";

        public const string Refund = "https://api.mch.weixin.qq.com/pay/refund";

        public static string SignTypeTransfer(WechatPaySignType signType)
        {
            switch (signType)
            {
                case WechatPaySignType.MD5 : return "MD5";
                case WechatPaySignType.HMACSHA256 : return "HMAC-SHA256";
                default : throw new ArgumentNullException(nameof(signType));
            }
        }
    }
}