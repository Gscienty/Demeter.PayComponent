using System.Security.Cryptography;
using System.Text;
using Gscienty.WechatPay.Entities;
using Gscienty.WechatPay.Defines;

namespace Gscienty.WechatPay.Services
{
    partial class WechatService
    {
        private const string ALPHA_DICTIONARY = "1234567890qwertyuiopasdfghjklzxcvbnmQWERTYUIOPASDFGHJKLZXCVBNM";

        private void PackageEntity<T>(T entity)
            where T : WechatBaseEntity
        {
            byte[] baseNonce = new byte[16];
            RandomNumberGenerator.Create().GetBytes(baseNonce);
            StringBuilder nonceBuilder = new StringBuilder();
            foreach(byte oneNonce in baseNonce)
            {
                nonceBuilder.Append(ALPHA_DICTIONARY[oneNonce % ALPHA_DICTIONARY.Length]);
            }

            (entity as WechatBaseEntity).AppId = this._settings.AppId;
            (entity as WechatBaseEntity).MerchantId = this._settings.MerchantId;
            (entity as WechatBaseEntity).Nonce = nonceBuilder.ToString();
            (entity as WechatBaseEntity).SignType = SignType.MD5;

            (entity as WechatBaseEntity).Sign = SignBuilder.GenerateSign<T>(entity, this._settings.SecurityKey);
        }
    }
}