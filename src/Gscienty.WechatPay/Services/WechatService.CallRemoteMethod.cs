using System.Net.Http;
using System.Text;
using System.Net;
using System.Security.Authentication;
using System.Security.Cryptography.X509Certificates;
using Gscienty.WechatPay.Entities;

namespace Gscienty.WechatPay.Services
{
    partial class WechatService
    {
        private T CallRemoteMethod<T>(string uri, string body)
            where T : WechatResponseBaseEntity, new()
        {
            HttpClient sender = new HttpClient();
            string result = sender.PostAsync(
                uri,
                new StringContent(body, Encoding.UTF8)
            ).Result.Content.ReadAsStringAsync().Result;
            
            sender.Dispose();            
            T responseEntity = XMLParser.Parser<T>(result);

            return responseEntity;
        }
        private T CallRemoteMethodSecurity<T>(string uri, string body)
            where T : WechatResponseBaseEntity, new()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.SslProtocols = SslProtocols.Tls12;
            handler.ClientCertificates.Add(this._settings.Certificate);

            HttpClient sender = new HttpClient(handler);
            string result = sender.PostAsync(
                uri,
                new StringContent(body, Encoding.UTF8)
            ).Result.Content.ReadAsStringAsync().Result;
            sender.Dispose();            
            T responseEntity = XMLParser.Parser<T>(result);

            return responseEntity;
        }
    }
}