using System;
using System.Threading.Tasks;
using Demeter.PayComponent.Wechat;
using Demeter.PayComponent.Wechat.RequestEntity;

namespace call
{
    class Program
    {
        static void Main(string[] args)
        {
            IWechatPay pay = new WechatPay(new WechatPaySettings
            {
                AppId = "wxd930ea5d5a258f4f",
                MerchantId = "10000100",
                AppSecurity = "3",
                SecurityKey = "192006250b4c09247ec02edce69f6a2d",
                PaymentNotifyURI = "",
                SignType = WechatPaySignType.HMACSHA256
            });

            var a = pay.Pay(new UnifiedOrderRequest
            {
                DeviceInfomation = "1000",
                Body = "test",
                OutTradeNumber = "1",
                TotalFee = 2,
                SpbillCreateIp = "1",
                TradeType = WechatPayTradeType.JSAPI
            });

            a.Wait();

            Console.WriteLine(a.Result.AppId);
        }
    }
}
