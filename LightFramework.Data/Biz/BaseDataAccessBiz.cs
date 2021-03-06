using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    /// <summary>
    /// 基本数据访问业务逻辑类的泛型基类。
    /// </summary>
    /// <typeparam name="U">数据访问层接口</typeparam>
    /// <typeparam name="T">实体对象</typeparam>
    public abstract class BaseDataAccessBiz<U, T> : CommonBiz<U,T>
        where U : IBaseDataAccess<T>
    {
        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="dataAccessor">数据访问器对象</param>
        protected BaseDataAccessBiz(U dataAccessor)
            : base(dataAccessor)
        {
        }

        /// <summary>
        /// 向数据库中插入一条记录,并返回标识值。
        /// </summary>
        /// <param name="entity">实体对象数据</param>
        /// <returns>自增标识值</returns>
        public virtual int AddWithId(T entity)
        {
            return this.DataAccessor.InsertWithId(entity);
        }

        /// <summary>
        /// 向数据库中插入一条记录。
        /// </summary>
        /// <param name="entity">实体对象数据</param>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        public virtual int Add(T entity)
        {
            return this.DataAccessor.Insert(entity);
        }

        /// <summary>
        /// 删除数据库表中所有数据。
        /// </summary>
        /// <returns>返回影响记录的行数,-1表示操作失败,大于-1表示成功</returns>
        public virtual int Clear()
        {
            return this.DataAccessor.DeleteAll();
        }
    }
}

