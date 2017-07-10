using System;

namespace Gscienty.WechatPay.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Class, Inherited = false, AllowMultiple = false)]
    sealed class IsRequisiteCertificateAttribute : System.Attribute
    {
        public IsRequisiteCertificateAttribute () { }
    }
}