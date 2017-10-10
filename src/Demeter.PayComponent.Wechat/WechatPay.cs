using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Net.Http;
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
        private const string ALPHA_DICTIONARY = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";


        public WechatPay(WechatPaySettings settings)
        {
            this._settings = settings;

            this._commonRequestEntity = new SortedList<string, string>();
            this._commonRequestEntity.Add("appid", this._settings.AppId);
            this._commonRequestEntity.Add("mch_id", this._settings.MerchantId);
            this._commonRequestEntity.Add("sign_type", Constant.SignTypeTransfer(this._settings.SignType));
        }

        void IWechatPay.Close()
        {
            throw new System.NotImplementedException();
        }

        async Task<UnifiedOrderResponse> IWechatPay.Pay(UnifiedOrderRequest unifiedOrder)
        {
            var requestEntity = this.GenerateCommonRequestEntity();
            requestEntity.Add("notify_url", this._settings.PaymentNotifyURI);

            this.FillRequestEntity(
                ref requestEntity,
                unifiedOrder,
                new SortedSet<string>(
                    new [] { "body", "out_trade_no", "total_fee", "spbill_create_ip", "trade_type"}
                )
            );

            return await this.CallAsync<UnifiedOrderResponse>(Constant.UnifiedOrder, requestEntity);
        }

        void IWechatPay.Query()
        {
            throw new System.NotImplementedException();
        }

        void IWechatPay.Refund()
        {
            throw new System.NotImplementedException();
        }

        private async Task<T> CallAsync<T>(string uri, SortedList<string, string> requestEntity)
            where T : class, new()
        {
            string postBody = this.GeneratePostBody(requestEntity);

            HttpClient sender = new HttpClient();
            var response = await sender.PostAsync(uri, new StringContent(postBody, Encoding.UTF8));
            string message = await response.Content.ReadAsStringAsync();
            sender.Dispose();
            
            return this.GenerateResponseEntity<T>(message);
        }

        private string GeneratePostBody(SortedList<string, string> requestEntity)
        {
            requestEntity.Add("sign", this.CalculateSign(requestEntity));
            XmlDocument requestDocument = new XmlDocument();
            XmlElement rootElement = requestDocument.CreateElement("xml");
            requestDocument.AppendChild(rootElement);

            foreach(var item in requestEntity)
            {
                XmlElement propertyElement = requestDocument.CreateElement(item.Key);
                propertyElement.InnerXml = item.Value;
                rootElement.AppendChild(propertyElement);
            }
            return requestDocument.InnerXml;
        }

        private T GenerateResponseEntity<T>(string xmlBody)
            where T : class, new()
        {
            T instance = new T();
            XmlDocument responseDocument = new XmlDocument();
            responseDocument.LoadXml(xmlBody);

            Dictionary<string, object> responseEntity = new Dictionary<string, object>();

            foreach(XmlNode child in responseDocument.DocumentElement.ChildNodes)
            {
                responseEntity.Add(child.Name, child.InnerText);
            }

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

        private void FillRequestEntity(
            ref SortedList<string, string> preSerialization,
            object entity,
            ISet<string> required)
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
                    preSerialization.Add(requestName.Name, value.ToString());
                }
            }
        }

        private string CalculateSign(SortedList<string, string> requestEntity)
        {
            StringBuilder stringA = new StringBuilder();
            bool isFirst = true;
                
            foreach(KeyValuePair<string, string> item in requestEntity)
            {
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

            if (this._settings.SignType == WechatPaySignType.MD5)
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

        private SortedList<string, string> GenerateCommonRequestEntity()
        {
            var result = new SortedList<string, string>(this._commonRequestEntity);
            result.Add("nonce_str", this.GenerateNonce());
            return result;
        }

        private string GenerateNonce()
        {
            byte[] baseNonce = new byte[16];
            RandomNumberGenerator.Create().GetBytes(baseNonce);
            StringBuilder nonceBuilder = new StringBuilder();
            foreach(byte oneNonce in baseNonce)
            {
                nonceBuilder.Append(ALPHA_DICTIONARY[oneNonce % ALPHA_DICTIONARY.Length]);
            }

            return nonceBuilder.ToString();
        }
    }
}