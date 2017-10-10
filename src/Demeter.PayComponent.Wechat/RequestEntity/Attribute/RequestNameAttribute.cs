using System;
namespace Demeter.PayComponent.Wechat.RequestEntity.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Property, Inherited = false, AllowMultiple = false)]
    internal sealed class RequestNameAttribute : System.Attribute
    {
        public string Name { get; private set; }
        public RequestNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}