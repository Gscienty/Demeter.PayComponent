using System;

namespace Gscienty.WechatPay.Attributes
{
    [AttributeUsage(System.AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    public sealed class IsRequisiteParameterAttribute : Attribute
    {
        public IsRequisiteParameterAttribute () { }
    }
}