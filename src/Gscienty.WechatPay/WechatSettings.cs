using System.Security.Cryptography.X509Certificates;

namespace Gscienty.WechatPay
{
    public sealed class WechatSettings
    {
        public string AppId { get; set; }
        public string MerchantId { get; set; }
        public string AppSecurity { get; set; }
        public string SecurityKey { get; set; }

        public X509Certificate2 Certificate { get; set; }
        public string CertificatePassword { get; set; }
    }
}