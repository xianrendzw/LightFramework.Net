using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace LightFramework.Data.SQLServer
{
    /// <summary>
    /// ViewDataAccess类提供对SQLServer数据库视图数据访问的抽象基类。
    /// </summary>
    /// <typeparam name="T">通用类型</typeparam>
    public abstract class ViewDataAccess<T> : BaseSelect<T>, IViewDataAccess<T> where T : BaseEntity
    {
        #region 构造函数

        /// <summary>
        /// 指定表名的构造函数,数据库连接字符串的构造函数。
        /// </summary>
        /// <param name="tableName">表名</param>
        /// <param name="connectionString">当前表的数据库连接字符串</param>
        protected ViewDataAccess(string tableName, string connectionString)
            : base(tableName, connectionString)
        {
        }

        #endregion
    }
}
