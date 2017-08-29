using System.Net.Http;
using Gscienty.WechatPay.Entities;
using Newtonsoft.Json;

namespace Gscienty.WechatPay.Services
{
    partial class WechatService
    {
        
        public WechatAccessToken GetAccessToken(string code)
        {
            string requestURI = $"https://api.weixin.qq.com/sns/oauth2/access_token?appid={this._settings.AppId}&secret={this._settings.AppSecurity}&code={code}&grant_type=authorization_code";
            
            string responseJson = (new HttpClient())
                .GetAsync(requestURI).Result.Content
                .ReadAsStringAsync().Result;
            if(responseJson.Contains("errcode"))
            {
                return null;
            }
            else
            {
                return JsonConvert.DeserializeObject<WechatAccessToken>(responseJson);
            }
        }
    }
}