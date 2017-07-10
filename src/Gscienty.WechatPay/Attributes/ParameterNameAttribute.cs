using System;

namespace Gscienty.WechatPay.Attributes
{
    [AttributeUsage(System.AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    internal sealed class ParameterNameAttribute : Attribute
    {
        readonly string _parameterName;
        
        public ParameterNameAttribute (string positionalString)
        {
            this._parameterName = positionalString;
        }
        
        public string ParameterName => this._parameterName;
    }
}