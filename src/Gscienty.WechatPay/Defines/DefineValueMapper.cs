using System;
using System.Collections.Generic;
using System.Reflection;
using Gscienty.WechatPay.Attributes;

namespace Gscienty.WechatPay.Defines
{
    internal sealed class DefineValueMapper
    {
        public static DefineValueMapper Instance { get; private set; }

        private Dictionary<Type, Dictionary<string, object>> _cache;

        private DefineValueMapper()
        {
            this._cache = new Dictionary<Type, Dictionary<string, object>>();
        }

        static DefineValueMapper()
        {
            Instance = new DefineValueMapper();
        }

        public object TransferValue(Type enumType, string value)
        {
            if(this._cache.ContainsKey(enumType) == false)
            {
                if(enumType.GetTypeInfo().IsEnum == false) { return null; }

                Dictionary<string, object> currentDict = new Dictionary<string, object>();

                foreach(FieldInfo field in enumType.GetRuntimeFields())
                {
                    DefineMapNameAttribute defineMapName = field
                        .GetCustomAttribute<DefineMapNameAttribute>();
                    if(defineMapName == null) { continue; }

                    currentDict.Add(defineMapName.Name, Enum.Parse(enumType, field.Name));
                }

                this._cache.Add(enumType, currentDict);
            }

            return this._cache[enumType][value];
        }
    }
}