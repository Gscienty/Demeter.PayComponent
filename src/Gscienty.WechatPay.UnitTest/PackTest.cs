using System;
using Xunit;

using Gscienty.WechatPay;
using Gscienty.WechatPay.Entities;

namespace Gscienty.WechatPay.UnitTest
{
    public class PackTest
    {
        [Fact]
        public void Test1()
        {
            Assert.Equal(XMLParser.Constructor(new RequestUnifiedOrderEntity()
            {
                Body = "test",
                TradeType = Defines.TradeType.MicroPay,
                NotifyURI = "http://tester.net/asd"
            }), "<xml><body>test</body><notify_url><![CDATA[http://tester.net/asd]]></notify_url><trade_type>MICROPAY</trade_type></xml>");
        }
    }
}
