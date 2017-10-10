using System.Threading.Tasks;
using Demeter.PayComponent.Wechat.RequestEntity;
using Demeter.PayComponent.Wechat.ResponseEntity;

namespace Demeter.PayComponent.Wechat
{
    public interface IWechatPay
    {
        Task<UnifiedOrderResponse> Pay(UnifiedOrderRequest unifiedOrder);

        void Query();

        void Close();

        void Refund();
    }
}