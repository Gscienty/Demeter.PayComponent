using System;
using Gscienty.WechatPay;
using Microsoft.Extensions.DependencyInjection;

namespace Gscienty.WechatPay.AspNetCore.Extension
{
    public static class WechatExtension
    {
        public static void UseWechatPay(this IServiceCollection services, Action<WechatSettings> settingAction)
        {
            WechatSettings settings = new WechatSettings();

            settingAction(settings);
            
            services.AddSingleton<IWechatPayManager>(
                factory => new WechatPayManager(settings)
            );
        }
    }
}