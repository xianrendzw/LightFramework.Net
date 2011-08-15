using System;
using System.Collections.Generic;
using System.Text;

namespace LightFramework.Data
{
    /// <summary>
    /// DataFieldMap类为提供实体对象与表的字段映射类。
    /// </summary>
    public class DataFieldMapTable : Dictionary<string,object>
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        public DataFieldMapTable()
            : base()
        {
            
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="map">映射接口</param>
        public DataFieldMapTable(IDictionary<string, object> map)
            : base(map)
        {
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="comparer">比较器对象</param>
        public DataFieldMapTable(IEqualityComparer<string> comparer)
            : base( comparer)
        {
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="capacity">初始化容量</param>
        public DataFieldMapTable(int capacity)
            : base(capacity)
        {
        }
    }
}
