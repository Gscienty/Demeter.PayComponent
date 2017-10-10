namespace Demeter.PayComponent.Wechat.ResponseEntity.Attribute
{
    [System.AttributeUsage(System.AttributeTargets.Property, Inherited = true, AllowMultiple = false)]
    internal sealed class ResponseNameAttribute : System.Attribute
    {
        public string Name { get; private set; }

        public ResponseNameAttribute(string name)
        {
            this.Name = name;
        }
    }
}