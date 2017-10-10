using System;
using Demeter.PayComponent.Wechat.RequestEntity.Attribute;

namespace Demeter.PayComponent.Wechat.RequestEntity
{
    public sealed class OrderQueryRequest
    {
        [RequestName("transaction_id")]
        public string TransactionID { get; set; }
        [RequestName("out_trade_no")]
        public string OutTradeNumber { get; set; }
    }
}