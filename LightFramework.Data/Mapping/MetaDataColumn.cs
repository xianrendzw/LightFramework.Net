using System;
using System.Reflection;

namespace LightFramework.Data
{
    /// <summary>
    /// 数据库表列的元数据。
    /// </summary>
    public class MetaDataColumn
    {
        private readonly PropertyInfo _member;
        private readonly ColumnAttribute _columnAttr;

        public MetaDataColumn(PropertyInfo member, ColumnAttribute columnAttr)
        {
            _member = member;
            _columnAttr = columnAttr;
        }

        public ColumnAttribute Attribute
        {
            get { return this._columnAttr; }
        }

        public Type DataType
        {
            get { return _member.PropertyType; }
        }

        public string Name
        {
            get { return _columnAttr.Name; }
        }

        public PropertyInfo Member
        {
            get { return _member; }
        }
    }
}
