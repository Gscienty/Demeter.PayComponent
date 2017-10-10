using Demeter.PayComponent.Wechat.ResponseEntity.Attribute;

namespace Demeter.PayComponent.Wechat.ResponseEntity
{
    public sealed class UnifiedOrderResponse : ResponseBase
    {
        [ResponseName("trade_type")]
        public string TradeType { get; set; }
        [ResponseName("prepay_id")]
        public string PrePayId { get; set; }        
        [ResponseName("code_url")]
        public string CodeURI { get; set; }
    }
}