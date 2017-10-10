using Demeter.PayComponent.Wechat.ResponseEntity.Attribute;

namespace Demeter.PayComponent.Wechat.ResponseEntity
{
    public abstract class ResponseBase
    {
        [ResponseName("appid")]
        public string AppId { get; set; }
        [ResponseName("mch_id")]
        public string MerchantId { get; set; }
        [ResponseName("nonce_str")]
        public string Nonce { get; set; }
        [ResponseName("nonce_str")]
        public string Sign { get; set; }
        [ResponseName("sign_type")]
        public string SignType { get; set; }
        [ResponseName("return_code")]
        public string ReturnCode { get; set; }
        [ResponseName("return_msg")]
        public string ReturnMessage { get; set; }
        [ResponseName("result_code")]
        public string ResultCode { get; set; }
        [ResponseName("err_code")]
        public string ErrorCode { get; set; }
        [ResponseName("err_code_des")]
        public string ErrorCodeDescription { get; set; }
    }
}