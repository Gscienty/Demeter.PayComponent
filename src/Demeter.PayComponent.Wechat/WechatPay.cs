using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Security.Authentication;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Net.Http;
using System.Net;
using System.IO;
using System.Threading.Tasks;
using Demeter.PayComponent.Wechat.RequestEntity;
using Demeter.PayComponent.Wechat.RequestEntity.Attribute;
using Demeter.PayComponent.Wechat.ResponseEntity;
using Demeter.PayComponent.Wechat.ResponseEntity.Attribute;


namespace Demeter.PayComponent.Wechat
{
    public class WechatPay : IWechatPay
    {
        private readonly WechatPaySettings _settings;
        private readonly SortedList<string, string> _commonRequestEntity;
        private readonly ISet<string> _payRequireProperties;
        private const string ALPHA_DICTIONARY = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";


        public WechatPay(WechatPaySettings settings)
        {
            this._settings = settings;

            this._commonRequestEntity = new SortedList<string, string>();
            this._commonRequestEntity.Add("appid", this._settings.AppId);
            this._commonRequestEntity.Add("mch_id", this._settings.MerchantId);
            this._commonRequestEntity.Add("sign_type", Constant.SignTypeTransfer(this._settings.SignType));

            this._payRequireProperties = new SortedSet<string>(
                new [] { "body", "out_trade_no", "total_fee", "spbill_create_ip", "trade_type"}
            );
        }

        void IWechatPay.Close()
        {
            throw new System.NotImplementedException();
        }

        //支付接口
        async Task<UnifiedOrderResponse> IWechatPay.Pay(UnifiedOrderRequest unifiedOrder)
        {
            //生成请求字段集合原料
            var requestEntity = this.GenerateCommonRequestEntity();
            requestEntity.Add("notify_url", this._settings.PaymentNotifyURI);

            //填充请求字段集合
            this.FillRequestEntity(ref requestEntity, unifiedOrder, this._payRequireProperties);

            //获取响应
            UnifiedOrderResponse response = await this.CallAsync<UnifiedOrderResponse>(
                Constant.UnifiedOrder, requestEntity);
            
            //检验响应体合法性
            if (this.CheckResponseLegal(response))
            {
                return response;
            }
            else
            {
                return null;
            }
        }

        void IWechatPay.Query()
        {
            throw new System.NotImplementedException();
        }

        void IWechatPay.Refund()
        {
            throw new System.NotImplementedException();
        }

        //检验响应体的合法性
        private bool CheckResponseLegal<T>(T entity)
            where T : ResponseBase, new()
        {
            SortedList<string, string> properies = new SortedList<string, string>();
            foreach (PropertyInfo property in typeof(T).GetRuntimeProperties())
            {
                ResponseNameAttribute requestName = property.GetCustomAttribute<ResponseNameAttribute>();
                if (requestName == null)
                {
                    throw new ArgumentOutOfRangeException(nameof(entity));
                }
                object value = property.GetValue(entity);
                if (value != null)
                {
                    properies.Add(requestName.Name, value.ToString());
                }
            }

            string sign = this.CalculateSign(properies, entity.SignType == null
                ? WechatPaySignType.MD5
                : entity.SignType == "MD5"
                    ? WechatPaySignType.MD5
                    : WechatPaySignType.HMACSHA256
            );

            return sign == entity.Sign;
        }

        //远程调用
        private async Task<T> CallAsync<T>(string uri, SortedList<string, string> requestProperies)
            where T : class, new()
        {
            //构造请求体Body部分
            string postBody = this.GeneratePostBody(requestProperies);

            //POST发送请求
            HttpClient sender = new HttpClient();
            var response = await sender.PostAsync(uri, new StringContent(postBody, Encoding.UTF8));

            //获取响应信息
            string message = await response.Content.ReadAsStringAsync();
            sender.Dispose();
            
            //构造并返回响应实体
            return this.GenerateResponseEntity<T>(message);
        }

        //通过X509证书双向认证的远程调用
        private async Task<T> SafelyCallAsync<T>(string uri, SortedList<string, string> requestProperies)
            where T : class, new()
        {
            //构造请求体Body部分
            string postBody = this.GeneratePostBody(requestProperies);

            //添加证书
            HttpClientHandler handler = new HttpClientHandler();
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;
            handler.SslProtocols = SslProtocols.Tls12;
            handler.ClientCertificates.Add(this._settings.Certificate);
            
            HttpClient sender = new HttpClient(handler);
            var response = await sender.PostAsync(uri, new StringContent(postBody, Encoding.UTF8));
            //获取响应信息
            string message = await response.Content.ReadAsStringAsync();
            sender.Dispose();
            
            //构造并返回响应实体
            return this.GenerateResponseEntity<T>(message);
        }

        //生成请求体Body部分
        private string GeneratePostBody(SortedList<string, string> requestProperies)
        {
            //计算签名
            requestProperies.Add("sign", this.CalculateSign(requestProperies, this._settings.SignType));

            XmlDocument requestDocument = new XmlDocument();
            XmlElement rootElement = requestDocument.CreateElement("xml");
            requestDocument.AppendChild(rootElement);

            foreach(var item in requestProperies)
            {
                XmlElement propertyElement = requestDocument.CreateElement(item.Key);
                propertyElement.InnerXml = item.Value;
                rootElement.AppendChild(propertyElement);
            }
            return requestDocument.InnerXml;
        }

        //生成响应体
        private T GenerateResponseEntity<T>(string xmlBody)
            where T : class, new()
        {
            T instance = new T();
            XmlDocument responseDocument = new XmlDocument();
            responseDocument.LoadXml(xmlBody);
            //将响应信息转换成XML格式后存入Dictionary
            Dictionary<string, object> responseEntity = new Dictionary<string, object>();
            foreach(XmlNode child in responseDocument.DocumentElement.ChildNodes)
            {
                responseEntity.Add(child.Name, child.InnerText);
            }
            //根据T类型的属性特征声明映射名称填充属性值
            foreach (PropertyInfo property in typeof(T).GetRuntimeProperties())
            {
                ResponseNameAttribute responseName = property.GetCustomAttribute<ResponseNameAttribute>();
                if (responseName == null)
                {
                    throw new InvalidOperationException();
                }
                if (responseEntity.ContainsKey(responseName.Name))
                {
                    property.SetValue(instance, responseEntity[responseName.Name]);
                }
            }

            return instance;
        }

        //从entity实体中获取所有属性填充到请求字段列表
        //entity为请求实体
        //required集合中记录了必填的字段名
        private void FillRequestEntity(
            ref SortedList<string, string> requestProperies, object entity, ISet<string> required)
        {
            foreach (PropertyInfo property in entity.GetType().GetRuntimeProperties())
            {
                RequestNameAttribute requestName = property.GetCustomAttribute<RequestNameAttribute>();
                if (requestName == null)
                {
                    throw new ArgumentOutOfRangeException(nameof(entity));
                }
                object value = property.GetValue(entity);
                if (required.Contains(requestName.Name) && value == null)
                {
                    throw new ArgumentNullException($"必填的字段\"{requestName.Name}\"为空");
                }
                if (value != null)
                {
                    requestProperies.Add(requestName.Name, value.ToString());
                }
            }
        }

        //根据属性列表计算签名
        private string CalculateSign(SortedList<string, string> properies, WechatPaySignType signType)
        {
            StringBuilder stringA = new StringBuilder();
            bool isFirst = true;
                
            foreach(KeyValuePair<string, string> item in properies)
            {
                if (item.Key == "sign")
                {
                    continue;
                }
                if(isFirst == false)
                {
                    stringA.Append("&");
                }
                else
                {
                    isFirst = false;
                }
                stringA.Append(item.Key);
                stringA.Append("=");
                stringA.Append(item.Value);
            }
            stringA.Append("&key=");
            stringA.Append(this._settings.SecurityKey);

            byte[] hash = null;

            if (signType == WechatPaySignType.MD5)
            {
                
                MD5 md5 = MD5.Create();
                md5.Initialize();

                hash = md5.ComputeHash(Encoding.UTF8.GetBytes(stringA.ToString()));

            }
            else
            {
                Console.WriteLine(stringA.ToString());
                KeyedHashAlgorithm hashAlgo = new HMACSHA256(
                    Encoding.UTF8.GetBytes(this._settings.SecurityKey)
                );
                hashAlgo.Initialize();

                hash = hashAlgo.ComputeHash(Encoding.UTF8.GetBytes(stringA.ToString()));
            }

            if (hash == null)
            {
                throw new InvalidOperationException(nameof(hash));
            }

            StringBuilder signBuilder = new StringBuilder();
            for(int i = 0; i < 32; i++)
            {
                signBuilder.Append(hash[i].ToString("X2"));
            }

            return signBuilder.ToString();
        }

        //生成通用请求字段列表
        private SortedList<string, string> GenerateCommonRequestEntity()
        {
            //生成单次值
            byte[] baseNonce = new byte[16];
            RandomNumberGenerator.Create().GetBytes(baseNonce);
            StringBuilder nonceBuilder = new StringBuilder();
            foreach(byte oneNonce in baseNonce)
            {
                nonceBuilder.Append(ALPHA_DICTIONARY[oneNonce % ALPHA_DICTIONARY.Length]);
            }

            var result = new SortedList<string, string>(this._commonRequestEntity);
            result.Add("nonce_str", nonceBuilder.ToString());
            return result;
        }
    }
}