using System;
using System.Text;
using System.Collections.Generic;
using System.Reflection;
using System.Security.Cryptography;
using Gscienty.WechatPay.Attributes;
using Gscienty.WechatPay.Defines;
using Gscienty.WechatPay.Entities;

namespace Gscienty.WechatPay
{
    public static class SignBuilder
    {
        private static string buildStringA<T>(T entity)
            where T : WechatBaseEntity
        {
            SortedList<string, string> parameters = new SortedList<string, string>();

            foreach(PropertyInfo property in entity.GetType().GetRuntimeProperties())
            {
                ParameterNameAttribute parameterName = property.GetCustomAttribute<ParameterNameAttribute>();
                if(parameterName == null || parameterName.ParameterName.Equals("sign")) { continue; }

                object value = null;
                if(property.PropertyType.GetTypeInfo().IsEnum)
                {
                    
                    DefineMapNameAttribute defineMapName = property
                        .PropertyType
                        .GetRuntimeField(property.GetValue(entity).ToString())
                        .GetCustomAttribute<DefineMapNameAttribute>();
                    if(defineMapName == null) { continue; }
                    value = defineMapName.Name;
                }
                else
                {
                    value = property.GetValue(entity);
                }

                if(value == null || string.IsNullOrEmpty(value.ToString())) { continue; }

                parameters.Add(parameterName.ParameterName, value.ToString());
            }
            
            StringBuilder stringA = new StringBuilder();
            bool isFirst = true;
                
            foreach(KeyValuePair<string, string> item in parameters)
            {
                if(isFirst == false) { stringA.Append("&"); } else { isFirst = false; }
                stringA.Append(item.Key);
                stringA.Append("=");
                stringA.Append(item.Value);
            }

            return stringA.ToString();
        }

        public static string GenerateSign<T>(T entity, string securityKey) where T : WechatBaseEntity
        {
            StringBuilder stringSignTemp = new StringBuilder(buildStringA(entity));
            stringSignTemp.Append("&key=");
            stringSignTemp.Append(securityKey);
            
            MD5 md5 = MD5.Create();
            md5.Initialize();

            byte[] hash = md5.ComputeHash(Encoding.UTF8.GetBytes(stringSignTemp.ToString()));
            
            StringBuilder signBuilder = new StringBuilder();
            for(int i = 0; i < 16; i++)
            {
                signBuilder.Append(hash[i].ToString("X2"));
            }

            return signBuilder.ToString();
        }
    }
}