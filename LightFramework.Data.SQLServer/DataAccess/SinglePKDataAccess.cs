using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.SqlClient;

namespace LightFramework.Data.SQLServer
{
    /// <summary>
    /// SinglePKDataAccess��ʵ�ֶ�SQLServer���ݿ��е����������ݷ��ʵĻ���������
    /// ��ʵ��ISinglePKDataAccess�ӿڡ�
    /// </summary>
    /// <typeparam name="T">ͨ������</typeparam>
    public abstract class SinglePKDataAccess<T> : BaseDataAccess<T>, ISinglePKDataAccess<T>
    {
        #region �ֶ�������

        /// <summary>
        /// ���ݿ�������ֶ�����
        /// </summary>
        protected string _primaryKey;

        /// <summary>
        /// ���ݿ�������ֶ���
        /// </summary>
        public string PrimaryKey
        {
            get
            {
                return this._primaryKey;
            }
        }

        #endregion

        #region ���캯��

        /// <summary>
        /// ָ�������Լ�����������,���ݿ������ַ����Ի�����й���,�˷���ֻ��Ե������ı�
        /// </summary>
        /// <param name="tableName">����</param>  
        /// <param name="primaryKey">��ĵ���������</param>
        /// <param name="connectionString">��ǰ������ݿ������ַ���</param>
        protected SinglePKDataAccess(string tableName, string primaryKey, string connectionString)
            : base(tableName, connectionString)
        {
            this._primaryKey = primaryKey;
        }

        #endregion

        #region ISinglePKDelete<T> ��Ա

        /// <summary>
        /// ����ָ�������ID,�����ݿ���ɾ��ָ������(������������)
        /// </summary>
        /// <param name="keyValue">ָ�������IDֵ</param>
        /// <returns>����Ӱ���¼������,-1��ʾ����ʧ��,����-1��ʾ�ɹ�</returns>
        public virtual int Delete(int keyValue)
        {
            string condition = string.Format("[{0}] = {1}", this._primaryKey, keyValue);
            return this.DeleteWithCondition(condition);
        }

        /// <summary>
        /// ����ָ�������ID,�����ݿ���ɾ��ָ������(������������)
        /// </summary>
        /// <param name="keyValue">ָ�������IDֵ</param>
        /// <returns>����Ӱ���¼������,-1��ʾ����ʧ��,����-1��ʾ�ɹ�</returns>
        public virtual int Delete(string keyValue)
        {
            string condition = string.Format("[{0}] = @p0", this._primaryKey);
            object[] parameterValues = new object[] { keyValue };

            return this.DeleteWithCondition(condition, parameterValues);
        }

        /// <summary>
        /// �����ݿ���ɾ��һ������ָ����ʶ�ļ�¼����������ʽ�ύ��
        /// </summary>
        /// <param name="keyValues">��¼��ʶ����</param>
        /// <returns>����Ӱ���¼������,-1��ʾ����ʧ��,����-1��ʾ�ɹ�</returns>
        public virtual int Delete(params int[] keyValues)
        {
            string[] keys = new string[keyValues.Length];
            for (int i = 0; i < keyValues.Length; i++)
            {
                keys[i] = keyValues[i].ToString();
            }

            return this.Delete(keys);
        }

        /// <summary>
        /// �����ݿ���ɾ��һ������ָ����ʶ�ļ�¼����������ʽ�ύ��
        /// </summary>
        /// <param name="keyValues">��¼��ʶ����</param>
        /// <returns>����Ӱ���¼������,-1��ʾ����ʧ��,����-1��ʾ�ɹ�</returns>
        public virtual int Delete(params string[] keyValues)
        {
            return this.DeleteIn(string.Join(",", keyValues).TrimEnd(','));
        }

        /// <summary>
        /// ɾ���������� ����Ϊ���϶��ָ���е�ֵ ʹ�� IN ƥ��
        /// </summary>
        /// <param name="keyValues">����IDֵ ��,�ŷָ�</param>
        /// <returns>����Ӱ���¼������,-1��ʾ����ʧ��,����-1��ʾ�ɹ�</returns>
        public virtual int DeleteIn(string keyValues)
        {
            string condition = string.Format("{0} IN({1})", this._primaryKey, keyValues.TrimEnd(','));
            return this.DeleteWithCondition(condition);
        }

        #endregion

        #region ISinglePKUpdate<T> ��Ա

        /// <summary>
        /// �������ݿ����ָ������ֵ�ļ�¼��
        /// </summary>
        /// <param name="entity">ʵ����󼯺�</param>
        /// <param name="id">����������ֵ</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ�������</param>
        /// <returns>����Ӱ���¼������,-1��ʾ����ʧ��,����-1��ʾ�ɹ�</returns>
        public virtual int Update(T entity, int id, params string[] columnNames)
        {
            string condition = String.Format("{0} = {1}", this._primaryKey, id);
            return this.UpdateWithCondition(entity, condition, null, columnNames);
        }

        /// <summary>
        /// �������ݿ����ָ������ֵ�ļ�¼��
        /// </summary>
        /// <param name="entity">ʵ����󼯺�</param>
        /// <param name="id">����������ֵ</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ�������</param>
        /// <returns>����Ӱ���¼������,-1��ʾ����ʧ��,����-1��ʾ�ɹ�</returns>
        public virtual int Update(T entity, string id, params string[] columnNames)
        {
            string condition = string.Format("[{0}] = @p0", this._primaryKey);
            object[] parameterValues = new object[] { id };

            return this.UpdateWithCondition(entity, condition, parameterValues, columnNames);
        }

        /// <summary>
        /// ����һ������ ����Ϊ����ָ���е�ֵ ʹ�� IN ƥ��
        /// ��ʾ��In������������������
        /// </summary>
        /// <param name="entity">���µ����ݶ���</param>
        /// <param name="keyValues">����IDֵ ��,�ŷָ�</param>
        /// <param name="columnNames">���µ�������(��Ӧ���ݶ���ʵ��)</param>
        /// <returns>����Ӱ���¼������,-1��ʾ����ʧ��,����-1��ʾ�ɹ�</returns>
        public virtual int UpdateIn(T entity, string keyValues, params string[] columnNames)
        {
            string condition = String.Format("{0} IN({1})", this._primaryKey, keyValues.TrimEnd(','));
            return this.UpdateWithCondition(entity, condition, null, columnNames);
        }

        #endregion

        #region ISinglePKSelect<T> ��Ա

        /// <summary>
        /// ��ѯ���ݿ�,�ж�ָ����ʶ�ļ�¼�Ƿ���ڡ�
        /// </summary>
        /// <param name="keyValues">ָ���ļ�¼��Ψһ��ʶ</param>
        /// <returns>�����򷵻�<c>true</c>������Ϊ<c>false</c>��</returns>
        public virtual bool IsExistKey(int keyValue)
        {
            string condition = string.Format("[{0}] = {1}", this._primaryKey, keyValue);
            return this.IsExistWithCondition(condition);
        }

        /// <summary>
        /// ��ѯ���ݿ�,�ж�ָ����ʶ�ļ�¼�Ƿ���ڡ�
        /// </summary>
        /// <param name="keyValues">ָ���ļ�¼��Ψһ��ʶ</param>
        /// <returns>�����򷵻�<c>true</c>������Ϊ<c>false</c>��</returns>
        public virtual bool IsExistKey(params int[] keyValues)
        {
            string[] keys = new string[keyValues.Length];
            for (int i = 0; i < keyValues.Length; i++)
            {
                keys[i] = keyValues[i].ToString();
            }

            return this.IsExistKey(keys);
        }

        /// <summary>
        /// ��ѯ���ݿ�,�ж�ָ����ʶ�ļ�¼�Ƿ���ڡ�
        /// </summary>
        /// <param name="keyValues">ָ���ļ�¼��Ψһ��ʶ</param>
        /// <returns>�����򷵻�<c>true</c>������Ϊ<c>false</c>��</returns>
        public virtual bool IsExistKey(string keyValue)
        {
            string condition = string.Format("[{0}] = @p0", this._primaryKey);
            object[] parameterValues = new object[] { keyValue };

            return this.IsExistWithCondition(condition, parameterValues);
        }

        /// <summary>
        /// ��ѯ���ݿ�,�ж�ָ����ʶ�ļ�¼�Ƿ���ڡ�
        /// </summary>
        /// <param name="keyValues">ָ���ļ�¼��Ψһ��ʶ</param>
        /// <returns>�����򷵻�<c>true</c>������Ϊ<c>false</c>��</returns>
        public virtual bool IsExistKey(params string[] keyValues)
        {
            return this.IsExistKey(string.Join(",", keyValues));
        }

        /// <summary>
        /// ��ѯ���ݿ�,�ж�ָ����ʶ�ļ�¼�Ƿ���ڣ�ʹ�� IN ƥ��
        /// </summary>
        /// <param name="keyValues">����IDֵ ��,�ŷָ�</param>
        /// <returns>ִ�гɹ�����<c>true</c>������Ϊ<c>false</c>��</returns>
        public virtual bool IsExistKeyIn(string keyValues)
        {
            string condition = string.Format("{0} IN({1})", this._primaryKey, keyValues.Trim(','));
            return this.IsExistWithCondition(condition);
        }

        /// <summary>
        /// �����ݿ��л�ȡ����ָ����ʶ��ʵ����󼯺�(����ֵ��Ҫ�ж��Ƿ�Ϊnull)��
        /// </summary>
        /// <param name="keyValue">��ʶֵ</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ�������</param>
        /// <returns>���ز�ѯ�������</returns>
        public virtual T Select(int keyValue, params string[] columnNames)
        {
            string condition = string.Format("WHERE [{0}] = {1}", this._primaryKey,keyValue);
            return this.SelectOne(condition, null, columnNames);
        }

        /// <summary>
        /// �����ݿ��л�ȡָ����ʶ��ʵ����󼯺Ͻ�֧������ID��ѯ(����ֵ��Ҫ�ж��Ƿ�Ϊnull)��
        /// </summary>
        /// <param name="keyValue">��ʶֵ (��֧������ID��ѯ)</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ�������</param>
        /// <returns>���ز�ѯ�������</returns>
        public virtual T Select(string keyValue, params string[] columnNames)
        {
            string condition = string.Format("WHERE [{0}] = @p0", this._primaryKey);
            return this.SelectOne(condition, this.GetParamerterValues(keyValue), columnNames);
        }

        /// <summary>
        /// �����ݿ��а����ID��ֵ��ҳ��С���л�ȡʵ����󼯺�(����ֵ����Ҫ�ж��Ƿ�Ϊnull),Ĭ�Ϸ��ذ����еı�ʶ�ֶν�������ļ��ϡ�
        /// </summary>
        /// <param name="pageSize">��ҳ��С������ҳ��ʾ��������¼</param>
        /// <param name="pageIndex">��ǰҳ��</param>
        /// <param name="condition">ָ��������,Ҫ���SQL���Where�ؼ��ֵ�����,�������Where�ؼ�����������Ч,��������ַ����к���SQL�������(@),�ұ���д�����¸�ʽ:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="orderByColumnName">�����ֶ����ƣ���Ҫ���ORDER BY�ؼ���,ֻҪָ�������ֶ����Ƽ���</param>
        /// <param name="sortType">SQL�����������</param>
        /// <param name="parameterValues">SQL������Ӧֵ�ļ���,��������ַ����к��в������,��������ø������ֵ</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ�������</param>
        /// <returns>��ǰ��ҳ������ȡʵ����󼯺�</returns>
        public virtual PageData<T> SelectWithPageSizeByIdentity(int pageSize, int pageIndex, string condition, string orderByColumnName, SortTypeEnum sortType, object[] parameterValues, params string[] columnNames)
        {
            if (!this.ContainWhere(condition))
                throw new ArgumentException("ָ��������,Ҫ���SQL���Where�ؼ��ֵ�����", "condition");

            if (pageSize < 1) pageSize = 10;
            if (pageIndex < 0) pageIndex = 0;
            //��ʱ���С
            int tempTableSize = pageIndex * pageSize;
            //����SQL����MAX������MIN�������ؿ�ֵʱ���滻ֵ
            int isNullValue = 0;

            //���õ������ʽ������Ϊȥ��Where�ַ���
            string param5 = string.IsNullOrEmpty(condition) ?
                 string.Empty :
                 string.Format("AND {0}", condition.Trim().Substring(5));

            string orderBy = string.IsNullOrEmpty(orderByColumnName) ?
               string.Format("[{0}] {1}", this._primaryKey, sortType.ToString()) :
               string.Format("[{0}] {1}", orderByColumnName, sortType.ToString());

            //��ȡɸѡ��
            string columns = this.GetColumns(columnNames);
            columns = string.Format("TOP {0} {1}", pageSize, columns);

            //��ҳ��ȡ���ݿ��¼����SQL���
            StringBuilder sqlFormat = new StringBuilder();
            if (sortType == SortTypeEnum.ASC)
            {
                sqlFormat.Append("SELECT {0} ");
                sqlFormat.Append("FROM [{1}] ");
                sqlFormat.Append("WHERE ([{2}] > ");
                sqlFormat.Append("(SELECT ISNULL(MAX([{2}]),{7}) ");
                sqlFormat.Append("FROM (SELECT TOP {3} [{2}] ");
                sqlFormat.Append("FROM [{1}] {4} ");
                sqlFormat.Append("ORDER BY {6}) AS TempTable) {5} ) ");
                sqlFormat.Append("ORDER BY {6}");
            }
            else
            {
                sqlFormat.Append("SELECT {0} ");
                sqlFormat.Append("FROM [{1}] ");
                sqlFormat.Append("WHERE ([{2}] < ");
                sqlFormat.Append("(SELECT ISNULL(MIN([{2}]),{7}) ");
                sqlFormat.Append("FROM (SELECT TOP {3} [{2}] ");
                sqlFormat.Append("FROM [{1}] {4} ");
                sqlFormat.Append("ORDER BY {6}) AS TempTable) {5} ) ");
                sqlFormat.Append("ORDER BY {6}");

                //����Ϊ�������ֵ
                isNullValue = int.MaxValue;
            }

            string sqlCmd = string.Format(sqlFormat.ToString(), columns, this._tableName,
                this._primaryKey, tempTableSize, condition, param5, orderBy, isNullValue);
            return this.GetPageData(condition, parameterValues, columnNames, sqlCmd, CommandType.Text);
        }

        /// <summary>
        /// ��ȡ���ݿ��иö�������IDֵ
        /// </summary>
        /// <returns>���IDֵ</returns>
        public virtual int GetMaxID()
        {
            return this.GetMaxValue(this._primaryKey, 10, string.Empty);
        }

        /// <summary>
        /// ��ȡ���ݿ��иö���ָ�����Ե����ֵ(û�м�¼��ʱ�򷵻�0)��
        /// </summary>
        /// <param name="fieldName">���е��ֶ�(��)����,�ֶε�ֵ��������������</param>
        /// <param name="fromBase">��(2,8,10,16)���Ƶ�����ת����10����</param>
        /// <param name="condition">Ҫ���SQL���Where�ؼ��ֵ�����,��������ַ����к���SQL�������(@),�ұ���д�����¸�ʽ:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL������Ӧֵ�ļ���,��������ַ����к��в������,��������ø������ֵ</param>
        /// <returns>ָ�����Ե����ֵ,û�м�¼��ʱ��Ϊ0</returns>
        public virtual int GetMaxValue(string fieldName, int fromBase, string condition, params object[] parameterValues)
        {
            if (!this.ContainWhere(condition))
                throw new ArgumentException("ָ��������,Ҫ���SQL���Where�ؼ��ֵ�����", "condition");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT MAX({0}) AS MaxValue FROM [{1}] {2}");

            string sqlCmd = string.Format(strSql.ToString(), fieldName, this._tableName, condition);
            SqlParameter[] parameters = this.FillParameters(parameterValues);
            object obj = SqlHelper.ExecuteScalar(this._connectionString, CommandType.Text, sqlCmd, parameters);
            return Convert.IsDBNull(obj) ? 0 : Convert.ToInt32(obj.ToString(), fromBase);
        }

        #endregion
    }
}