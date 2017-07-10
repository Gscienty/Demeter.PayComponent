using System;

namespace Gscienty.WechatPay.Attributes
{
    [System.AttributeUsage(System.AttributeTargets.Field, Inherited = false, AllowMultiple = false)]
    sealed class DefineMapNameAttribute : System.Attribute
    {
        readonly string _name;
        
        public DefineMapNameAttribute (string name)
        {
            this._name = name;
        }
        
        public string PositionalString => this._name;
    }
}