using Gscienty.WechatPay.Attributes;

namespace Gscienty.WechatPay.Defines
{
    public enum ErrorCodeType
    {
        [DefineMapName("NOAUTH")]
        NoAuthorization,

        [DefineMapName("NOENOUGH")]
        NotEnough,

        [DefineMapName("ORDERCLOSED")]
        OrderClosed,

        [DefineMapName("SYSTEMERROR")]
        SystemError,

        [DefineMapName("APPID_NOT_EXIST")]
        AppIDNotExist,

        [DefineMapName("MCHID_NOT_EXIST")]
        MerchantIDNotExist,

        [DefineMapName("APPID_MCHID_NOT_MATCH")]
        AppIDMerchantIDNotMatch,

        [DefineMapName("LACK_PARAMS")]
        LackParameters,

        [DefineMapName("OUT_TRADE_NO_USED")]
        OutTradeNoUsed,

        [DefineMapName("SIGNERROR")]
        SignError,

        [DefineMapName("XML_FORMAT_ERROR")]
        XMLFormatError,

        [DefineMapName("REQUIRE_POST_METHOD")]
        RequirePostMethod,

        [DefineMapName("POST_DATA_EMPTY")]
        PostDataEmpty,

        [DefineMapName("NOT_UTF8")]
        NotUTF8
    }
}