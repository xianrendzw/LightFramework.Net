using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using System.Xml;
using System.Xml.Serialization;
using System.Reflection;

namespace JingQiao.HNCE.DTO
{
    /// <summary>
    /// 数据实体基类。
    /// </summary>
    [Serializable]
    public abstract class BaseEntity
    {
        #region Indexer Members

        /// <summary>
        /// 获取指定属性值的索引器。
        /// </summary>
        /// <param name="propertyName">属性名称,区分大小写</param>
        /// <returns>属性值</returns>
        public object this[string propertyName]
        {
            get
            {
                Type type = this.GetType();
                PropertyInfo pi = type.GetProperty(propertyName);
                if (pi != null) return pi.GetValue(this, null);
                return null;
            }
            set
            {
                Type type = this.GetType();
                PropertyInfo pi = type.GetProperty(propertyName);
                if (pi != null) pi.SetValue(this, value, null);
            }
        }

        /// <summary>
        /// 排除实体对象指定一系列常量值后的集合。
        /// </summary>
        /// <param name="columnNames">要排除常量值的集合</param>
        /// <returns>排除后该对象常量值的集合</returns>
        public string[] Except(params string[] columnNames)
        {
            FieldInfo[] fields = this.GetType().GetFields(BindingFlags.Static);

            if (fields == null ||
                fields.Length == 0) return null;

            //如果没有排除项则返回所有常量值
            if (columnNames == null ||
                columnNames.Length == 0)
            {
                return fields.Select(field => field.GetValue(this).ToString()).ToArray();
            }

            var lowerConsts = columnNames.Select(c => c.ToLower());
            var exclueFields = fields.Where(field => lowerConsts.Contains(field.GetValue(this).ToString().ToLower()));
            return exclueFields.Select(field => field.GetValue(this).ToString()).ToArray();
        }

        #endregion
    }
}

