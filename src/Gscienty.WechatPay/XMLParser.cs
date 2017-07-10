using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using Gscienty.WechatPay.Entities;
using Gscienty.WechatPay.Attributes;

namespace Gscienty.WechatPay
{
    public sealed class XMLParser
    {
        private Stack<string> _xmlNodeStack;
        private StringBuilder _xmlBuilder;

        private string Result => this._xmlBuilder.ToString();

        private XMLParser()
        {
            this._xmlNodeStack = new Stack<string>();
            this._xmlBuilder = new StringBuilder();
        }

        public static string Constructor<T>(T entity) where T : WechatBaseEntity
        {
            XMLParser parser = new XMLParser();

            parser.In("xml");

            foreach(PropertyInfo property in entity.GetType().GetRuntimeProperties())
            {
                ParameterNameAttribute parameterName = property.GetCustomAttribute<ParameterNameAttribute>();

                if(parameterName == null) { throw new Exception("Lack parameter metadata"); }

                object value = property.GetValue(entity);
                if(value == null) { continue; }
                    parser.SetKeyValuePair(
                        parameterName.ParameterName,
                        value.GetType().GetTypeInfo().IsEnum ?
                            value.GetType()
                            .GetRuntimeField(value.ToString())
                            .GetCustomAttribute<DefineMapNameAttribute>()
                            .Name :
                            value.ToString()
                    );
            }

            parser.Out();
            return parser.Result;
        }

        private void In(string nodeName)
        {
            this._xmlNodeStack.Push(nodeName);
            this._xmlBuilder.Append($"<{nodeName}>");
        }
        
        private void Out()
        {
            this._xmlBuilder.Append($"</{this._xmlNodeStack.Pop()}>");
        }

        private void SetKeyValuePair(string key, string value)
        {
            this.In(key);
            if(value.Contains("<") || value.Contains(">") || value.Contains("^") || value.Contains("'") || value.Contains("\"") || value.Contains("\\") || value.Contains("/"))
            {
                this.SetCDATAValue(value);
            }
            else
            {
                this.SetValue(value);
            }
            this.Out();
        }

        public void SetValue(string value)
        {
            this._xmlBuilder.Append(value
                .Replace("<", "&lt;")
                .Replace(">", "&gt;")
                .Replace("&", "&amp;")
                .Replace("'", "&apos;")
                .Replace("\"", "&quot;")
            );
        }

        public void SetCDATAValue(string value)
        {
            this._xmlBuilder.Append($"<![CDATA[{value}]]>");
        }
    }
}