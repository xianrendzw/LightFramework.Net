using System;
using System.Collections.Generic;
using System.Text;

namespace LightFramework.Data
{
    /// <summary>
    /// DataFieldMap��Ϊ�ṩʵ����������ֶ�ӳ���ࡣ
    /// </summary>
    public class DataFieldMapTable : Dictionary<string,object>
    {
        /// <summary>
        /// ���캯����
        /// </summary>
        public DataFieldMapTable()
            : base()
        {
            
        }

        /// <summary>
        /// ���캯����
        /// </summary>
        /// <param name="map">ӳ��ӿ�</param>
        public DataFieldMapTable(IDictionary<string, object> map)
            : base(map)
        {
        }

        /// <summary>
        /// ���캯����
        /// </summary>
        /// <param name="comparer">�Ƚ�������</param>
        public DataFieldMapTable(IEqualityComparer<string> comparer)
            : base( comparer)
        {
        }

        /// <summary>
        /// ���캯����
        /// </summary>
        /// <param name="capacity">��ʼ������</param>
        public DataFieldMapTable(int capacity)
            : base(capacity)
        {
        }
    }
}
