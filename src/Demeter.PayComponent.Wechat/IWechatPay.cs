using System.Threading.Tasks;
using Demeter.PayComponent.Wechat.RequestEntity;

namespace Demeter.PayComponent.Wechat
{
    public interface IWechatPay
    {
        Task<string> Pay(UnifiedOrder unifiedOrder);

        void Query();

        void Close();

        void Refund();
    }
}