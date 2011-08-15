using System.Collections.Generic;
using System.Data;

namespace LightFramework.Data
{
    /// <summary>
    /// 分页查询记录集类。
    /// </summary>
    /// <typeparam name="T">通类类型</typeparam>
    public class PageData<T>
    {
        /// <summary>
        /// 查询返回的分页记录集
        /// </summary>
        private List<T> _recordSet = null;

        /// <summary>
        /// 分页总数。
        /// </summary>
        private int _count;

        /// <summary>
        /// 当前分页包括的记录区间(如：1-5)
        /// </summary>
        private string _currentRecordInterval;

        /// <summary>
        /// 获取或设置查询返回的分页记录集
        /// </summary>
        public List<T> RecordSet
        {
            get { return this._recordSet; }
            set { this._recordSet = value; }
        }

        /// <summary>
        /// 获取或设置符合查询条件总记录数
        /// </summary>
        public int Count
        {
            get { return this._count; }
            set { this._count = value; }
        }

        /// <summary>
        /// 获取或设置当前分页包括的记录区间(如：1-5)
        /// </summary>
        public string CurrentRecordInterval
        {
            get { return this._currentRecordInterval; }
            set { this._currentRecordInterval = value; }
        }
    }
}
