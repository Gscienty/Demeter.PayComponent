using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Reflection;
using Gscienty.WechatPay.Defines;
using Gscienty.WechatPay.Entities;
using Gscienty.WechatPay.Attributes;

namespace Gscienty.WechatPay
{
    public sealed class XMLParser
    {
        private static readonly Regex CDATA_REGEX = new Regex(@"^<\!\[CDATA\[(.*?)\]\]>$");

        private static Object transferValueByType(Type type, String data)
        {
            if(CDATA_REGEX.IsMatch(data)) { return CDATA_REGEX.Match(data).Groups[1].Value; }
            else { return data; }
        }

        public static T Parser<T>(string xmlString) where T : WechatBaseEntity, new()
        {

            T result = new T();
            XmlTreeNode treeRoot = new XmlTreeNode(xmlString);

            foreach(PropertyInfo property in typeof(T).GetRuntimeProperties())
            {
                ParameterNameAttribute parameterName = property.GetCustomAttribute<ParameterNameAttribute>();

                if(parameterName == null) { continue; }

                XmlTreeNode currentNode = treeRoot.Child(parameterName.ParameterName);
                if(currentNode == null || currentNode.IsLeaf == false) { continue; }
                if(property.PropertyType.GetTypeInfo().IsEnum)
                {
                    property.SetValue(
                        result,
                        DefineValueMapper.Instance.TransferValue(
                            property.PropertyType,
                            currentNode.Value.Trim()
                        )
                    );
                }
                else if(property.PropertyType == typeof(int?))
                {
                    property.SetValue(
                        result,
                        Convert.ToInt32(
                            transferValueByType(property.PropertyType, currentNode.Value.Trim())
                        )
                    );
                }
                else
                {
                    property.SetValue(
                        result,
                        transferValueByType(property.PropertyType, currentNode.Value.Trim())
                    );
                }
            }
            return result;
        }

        private sealed class XmlTreeNode
        {
            public string Root { get; private set; }
            public string Value { get; private set; }
            public bool IsLeaf { get; private set; }
            private Dictionary<string, XmlTreeNode> _children;
            public XmlTreeNode(string xmlString)
            {
                this._children = new Dictionary<string, XmlTreeNode>();

                this.Initialize(xmlString);
            }

            public XmlTreeNode Child(string name)
            {
                if(this._children.ContainsKey(name)) { return this._children[name]; }
                else { return null; }
            }

            public void EachChild(Action<XmlTreeNode> eachor)
            {
                foreach(XmlTreeNode child in this._children.Values) { eachor(child); }
            }

            private void Initialize(string xmlString)
            {
                Tuple<string, string> cutResult = this.getNode(xmlString, 0);
                this.Root = cutResult.Item1;

                List<string> children = this.getChildren(cutResult.Item2);
                if(children.Count == 0)
                {
                    this.IsLeaf = true;
                    this.Value = cutResult.Item2.Trim();
                }
                else
                {
                    this.IsLeaf = false;
                    foreach(string child in children)
                    {
                        XmlTreeNode childNode = new XmlTreeNode(child);

                        this._children.Add(childNode.Root, childNode);
                    }
                }
            }

            private List<string> getChildren(string xmlString)
            {
                List<String> result = new List<String>();

                string nodeName = string.Empty;
                int nodeNameIndex = 0;
                int nodeStart = -1;
                int nameIndex = -1;
                int status = 0;

                for(int i = 0; i < xmlString.Length; i++)
                {
                    if(status == 0 && xmlString[i] == '<') {
                        status = 1;
                        nodeStart = i;
                    }
                    else if(status == 1) { status = 2; nameIndex = i; }
                    else if(status == 2 && xmlString[i] == '>')
                    {
                        status = 3;
                        nodeName = xmlString.Substring(nameIndex, i - nameIndex);
                    }
                    else if(status == 3 && xmlString[i] == '<') { status = 4; }
                    else if(status == 4 && xmlString[i] == '/') {
                        status = 5;
                        nodeNameIndex = 0;
                    }
                    else if(status == 4 && xmlString[i] != '/') { status = 3; }
                    else if(status == 5 && xmlString[i] == nodeName[nodeNameIndex])
                    {
                        nodeNameIndex++;
                        if(nodeNameIndex == nodeName.Length)
                        {
                            status = 6;
                        }
                    }
                    else if(status == 5 && xmlString[i] != nodeName[nodeNameIndex]) { status = 3; }
                    else if(status == 6 && xmlString[i] == '>') {
                        result.Add(xmlString.Substring(nodeStart, i + 1 - nodeStart));
                        
                        nodeName = string.Empty;
                        nodeStart = -1;
                        nodeNameIndex = 0;
                        nameIndex = -1;
                        status = 0;
                    }
                    else if(status == 6 && xmlString[i] != '>') { status = 3; }
                }

                return result;
            }

            private Tuple<string, string> getNode(string xmlString, int offset)
            {
                string nodeName = string.Empty;
                int nodeNameIndex = 0;
                int nameIndex = -1;
                int innerStartIndex = -1;
                int innerEndIndex = -1;
                int status = 0;

                for(int i = offset; i < xmlString.Length; i++)
                {
                    if(status == 0 && xmlString[i] == '<') { status = 1; }
                    else if(status == 1) { status = 2; nameIndex = i; }
                    else if(status == 2 && xmlString[i] == '>')
                    {
                        status = 3;
                        nodeName = xmlString.Substring(nameIndex, i - nameIndex);
                        innerStartIndex = i + 1;
                    }
                    else if(status == 3 && xmlString[i] == '<')
                    {
                        status = 4;
                        innerEndIndex = i - 1;
                    }
                    else if(status == 4 && xmlString[i] == '/') {
                        status = 5;
                        nodeNameIndex = 0;
                    }
                    else if(status == 4 && xmlString[i] != '/') { status = 3; }
                    else if(status == 5 && xmlString[i] == nodeName[nodeNameIndex])
                    {
                        nodeNameIndex++;
                        if(nodeNameIndex == nodeName.Length)
                        {
                            status = 6;
                        }
                    }
                    else if(status == 5 && xmlString[i] != nodeName[nodeNameIndex]) { status = 3; }
                    else if(status == 6 && xmlString[i] == '>') {
                        status = 7;
                        break;
                    }
                    else if(status == 6 && xmlString[i] != '>') { status = 3; }
                }

                if(innerStartIndex == -1 || innerEndIndex == -1)
                {
                    return Tuple.Create(String.Empty, String.Empty);
                }
                else
                {
                    return Tuple.Create(
                        nodeName,
                        xmlString.Substring(innerStartIndex, innerEndIndex + 1 - innerStartIndex)
                    );
                }
            }
        }
    }
}