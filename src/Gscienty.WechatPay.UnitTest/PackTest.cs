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
            Assert.Equal(XMLConstructor.Constructor(new RequestUnifiedOrderEntity()
            {
                Body = "test",
                TradeType = Defines.TradeType.JSAPI,
                NotifyURI = "http://tester.net/asd"
            }), "<xml><body>test</body><notify_url><![CDATA[http://tester.net/asd]]></notify_url><trade_type>JSAPI</trade_type></xml>");
        }

        [Fact]
        public void Test2()
        {
            Assert.Equal(XMLParser.Parser<RequestUnifiedOrderEntity>(
                "<xml><body>test</body><notify_url><![CDATA[http://tester.net/asd]]></notify_url><trade_type>NATIVE</trade_type></xml>"
            ).TradeType, Defines.TradeType.Native);
        }

        [Fact]
        public void Test3()
        {
            RequestUnifiedOrderEntity entity = XMLParser.Parser<RequestUnifiedOrderEntity>(
                "<xml><appid>wx2421b1c4370ec43b</appid><attach>支付测试</attach><body>JSAPI支付测试</body>" +
                "<mch_id>10000100</mch_id><detail><![CDATA[{ \"goods_detail\":" + 
                "[ { \"goods_id\":\"iphone6s_16G\", \"wxpay_goods_id\":\"1001\", \"goods_name\":\"iPhone6s 16G\", \"quantity\":1, \"price\":528800, \"goods_category\":\"123456\", \"body\":\"苹果手机\" }, { \"goods_id\":\"iphone6s_32G\", \"wxpay_goods_id\":\"1002\", \"goods_name\":\"iPhone6s 32G\", \"quantity\":1, \"price\":608800, \"goods_category\":\"123789\", \"body\":\"苹果手机\" } ] }]]></detail>" +
                "<nonce_str>1add1a30ac87aa2db72f57a2375d8fec</nonce_str>" +
                "<notify_url>http://wxpay.wxutil.com/pub_v2/pay/notify.v2.php</notify_url>" +
                "<openid>oUpF8uMuAJO_M2pxb1Q9zNjWeS6o</openid>" +
                "<out_trade_no>1415659990</out_trade_no>" +
                "<spbill_create_ip>14.23.150.211</spbill_create_ip>" +
                "<total_fee>1</total_fee>" +
                "<trade_type>JSAPI</trade_type>" +
                "<sign>30FBD753404DDE4EC124D6A8AE8AB2CE</sign>" +
                "</xml>");

            Assert.Equal(SignBuilder.GenerateSign(entity, "123123"), "30FBD753404DDE4EC124D6A8AE8AB2CE");
        }
    }
}
