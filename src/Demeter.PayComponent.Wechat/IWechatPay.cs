using System.Threading.Tasks;
using Demeter.PayComponent.Wechat.RequestEntity;
using Demeter.PayComponent.Wechat.ResponseEntity;

namespace Demeter.PayComponent.Wechat
{
    public interface IWechatPay
    {
        Task<UnifiedOrderResponse> Pay(UnifiedOrderRequest unifiedOrder);

        Task<OrderQueryResponse> Query(OrderQueryRequest orderQuery);

        void Close();

        Task<RefundResponse> Refund(RefundRequest refund);
    }
}