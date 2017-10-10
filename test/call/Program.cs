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
                AppId = "1",
                MerchantId = "2",
                AppSecurity = "3",
                SecurityKey = "4",
                PaymentNotifyURI = "5"
            });

            var a = pay.Pay(new UnifiedOrder
            {
                Body = "6",
                OutTradeNumber = "7",
                TotalFee = 8,
                SpbillCreateIp = "9",
                TradeType = "10"
            });

            a.Wait();

            Console.WriteLine(a.Result);
        }
    }
}
