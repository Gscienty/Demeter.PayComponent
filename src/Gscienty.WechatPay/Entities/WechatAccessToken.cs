using Newtonsoft.Json;

namespace Gscienty.WechatPay.Entities
{
    public sealed class WechatAccessToken
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("expres_in")]
        public int ExpresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }

        [JsonProperty("openid")]
        public string OpenId { get; set; }
        
        [JsonProperty("scope")]
        public string Scope { get; set; }
    }
}