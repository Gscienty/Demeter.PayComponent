using System;

namespace Gscienty.WechatPay.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    sealed class ParameterMaxLengthAttribute : System.Attribute
    {
        readonly int _maxLength;
        
        public ParameterMaxLengthAttribute (int maxLength)
        {
            this._maxLength = maxLength;
        }
        
        public int PositionalString => this._maxLength;
    }
}