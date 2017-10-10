using System.Security.Cryptography.X509Certificates;

namespace Demeter.PayComponent.Wechat
{
    public sealed class WechatPaySettings
    {
        public string AppId { get; set; }
        public string MerchantId { get; set; }
        public string AppSecurity { get; set; }
        public string SecurityKey { get; set; }
        public string PaymentNotifyURI { get; set; }
        public X509Certificate2 Certificate { get; set; }
        public string CertificatePassword { get; set; }
    }
}