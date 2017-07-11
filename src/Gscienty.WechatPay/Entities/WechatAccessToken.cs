using Newtonsoft.Json;

namespace Gscienty.WechatPay.Entities
{
    public sealed class WechatAccessToken
    {
        [JsonPropertyAttribute("access_token")]
        public string AccessToken { get; set; }

        [JsonPropertyAttribute("expres_in")]
        public int ExpresIn { get; set; }

        [JsonPropertyAttribute("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonPropertyAttribute("openid")]
        public string OpenId { get; set; }
        
        [JsonPropertyAttribute("scope")]
        public string Scope { get; set; }
    }
}