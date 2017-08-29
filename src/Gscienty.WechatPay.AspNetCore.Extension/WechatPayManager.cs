using Gscienty.WechatPay;
using Gscienty.WechatPay.Services;
using Gscienty.WechatPay.Entities;

namespace Gscienty.WechatPay.AspNetCore.Extension
{
    public sealed class WechatPayManager : IWechatPayManager
    {
        private WechatService _service;

        public WechatPayManager(WechatSettings settings)
        {
            this._service = new WechatService(settings);
        }

        ResponseUnifiedOrderEntity IWechatPayManager.UnifiedOrder(
            RequestUnifiedOrderEntity entity) => this._service.UnifiedOrder(entity);

        ResponseRefundEntity IWechatPayManager.Refund(
            RequestRefundEntity entity) => this._service.Refund(entity);

        ResponseOrderQueryEntity IWechatPayManager.QueryOrder(
            RequestOrderQueryEntity entity) => this._service.QueryOrder(entity);

        PayNotifyEntity IWechatPayManager.PayNotify(string responseString) =>
            this._service.PayNotify(responseString);

        ResponseCloseOrderEntity CloseOrder(RequestCloseOrderEntity entity) =>
            this._service.CloseOrder(entity);
            
        WechatAccessToken IWechatPayManager.GetAccessToken(string code) =>
            this._service.GetAccessToken(code);

        ResponseCloseOrderEntity IWechatPayManager.CloseOrder(RequestCloseOrderEntity entity) =>
            this._service.CloseOrder(entity);
        
    }
}