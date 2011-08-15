using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.Serialization;

namespace LightFramework.Web
{
    public class DhtmlxTreeHelper
    {
        /// <summary>
        /// 生成dhtmlxTree树动态加载的JSON结点的字符串
        /// </summary>
        /// <param name="id"></param>
        /// <param name="nodes"></param>
        /// <returns></returns>
        public static string GetDhtmlxTreeJSONNodes(int id, List<DhtmlTreeNode> nodes)
        {
            string JSONTextFormat = "{{id:\"{0}\", item:[{1}]}}";
            string JSONNodeFormat = "{{id:\"{0}\",text:\"{1}\",tooltip:\"{2}\",child:{3}}},";
            StringBuilder jsonString = new StringBuilder();

            foreach (DhtmlTreeNode dto in nodes)
            {
                string JOSNNodeStr = string.Format(JSONNodeFormat, dto.Id, dto.Text, dto.ToolTip,
                    dto.Child);

                jsonString.Append(JOSNNodeStr);
            }

            return string.Format(JSONTextFormat, id, jsonString.ToString().TrimEnd(','));
        }
    }

    /// <summary>
    /// Dhtmltree(http://www.dhtmlx.com/docs/products/dhtmlxTree/index.shtml)树的节点的类
    /// </summary>
    [DataContract]
    public class DhtmlTreeNode
    {
        private int _id;
        private string _text;
        private string _toolTip;
        private bool _checked;
        private byte _child = 0;
        private List<DhtmlTreeNode> _nodes;

        public DhtmlTreeNode()
        { }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">节点标识</param>
        /// <param name="text">节点显示的文本</param>
        public DhtmlTreeNode(int id, string text)
        {
            this._id = id;
            this._text = text;
            this._toolTip = text;
        }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="id">节点标识</param>
        /// <param name="text">节点显示的文本</param>
        /// <param name="child">节点是否有子节点,0表示没有,1表示有</param>
        public DhtmlTreeNode(int id, string text, byte child)
            : this(id, text)
        {
            this._child = child;
        }

        /// <summary>
        /// 获取或设置节点标识
        /// </summary>
        [DataMember(Name = "id")]
        public int Id
        {
            get { return this._id; }
            set { this._id = value; }
        }

        /// <summary>
        /// 获取或设置点地显示的文本
        /// </summary>
        [DataMember(Name = "text")]
        public string Text
        {
            get { return this._text; }
            set { this._text = value; }
        }

        /// <summary>
        /// 获取或设置节点的工具提示文本。
        /// </summary>
        [DataMember(Name = "tooltip")]
        public string ToolTip
        {
            get { return this._toolTip; }
            set { this._toolTip = value; }
        }

        /// <summary>
        /// 获取或设置一个值，该值指示节点的复选框是否被选中
        /// </summary>
        [DataMember(Name = "checked")]
        public bool Checked
        {
            get { return this._checked; }
            set { this._checked = value; }
        }

        /// <summary>
        /// 获取或设置节点是否有子节点,0表示没有,1表示有
        /// </summary>
        [DataMember(Name = "child")]
        public byte Child
        {
            get { return this._child; }
            set { this._child = value; }
        }

        /// <summary>
        /// 获取或设置获取 DhtmlTreeNode 集合，该集合包含当前节点的第一级子节点
        /// </summary>
        [DataMember(Name = "item")]
        public List<DhtmlTreeNode> Nodes
        {
            get { return this._nodes; }
            set { this._nodes = value; }
        }
    }
}