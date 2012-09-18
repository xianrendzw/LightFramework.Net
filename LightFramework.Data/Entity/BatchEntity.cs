using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    /// <summary>
    /// 批量实体类。
    /// </summary>
    /// <typeparam name="T">BaseEntity</typeparam>
    public class BatchEntity<T> where T : BaseEntity
    {
        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="destTableName">插入数据库时所对应的表名</param>
        public BatchEntity(T entity,string destTableName) {
            this.Entity = entity;
            this.DestTableName = destTableName;
        }

        /// <summary>
        /// 构造函数。
        /// </summary>
        /// <param name="entity">实体对象</param>
        /// <param name="destTableName">插入数据库时所对应的表名</param>
        /// <param name="columnNames">实体插入数据库时所需的列名</param>
        public BatchEntity(T entity, string destTableName, string[] columnNames)
            : this(entity, destTableName)
        {
            this.ColumnNames = columnNames;
        }

        /// <summary>
        /// 获取与设置当前实体对象
        /// </summary>
        public T Entity { get; set; }

        /// <summary>
        /// 获取与设置当前实体插入数据库时所对应的表名
        /// </summary>
        public string DestTableName { get; set; }

        /// <summary>
        /// 获取与设置当前实体插入数据库时所需的列名
        /// </summary>
        public virtual string[] ColumnNames { get; set; }
    }
}
