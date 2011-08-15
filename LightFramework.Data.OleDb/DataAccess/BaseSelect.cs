using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using System.Data;
using System.Data.OleDb;

namespace LightFramework.Data.OleDb
{
    /// <summary>
    /// BaseSelect���ṩ��OleDb���ݿ�һЩ�����Ĳ�ѯ�����ĳ�����ࡣ
    /// </summary>
    /// <typeparam name="T">ͨ������</typeparam>
    public abstract class BaseSelect<T> : IBaseSelect<T>
    {
        #region ���캯��

        /// <summary>
        /// ָ�������Ĺ��캯��,���ݿ������ַ����Ĺ��캯����
        /// </summary>
        /// <param name="tableName">����</param>
        /// <param name="connectionString">��ǰ�������ݿ������ַ���</param>
        protected BaseSelect(string tableName, string connectionString)
        {
            this._tableName = tableName;
            this._connectionString = connectionString;
        }

        #endregion

        #region ��������

        /// <summary>
        /// ��Ҫ��ʼ���Ķ��������
        /// </summary>
        protected string _tableName = string.Empty;

        /// <summary>
        /// ��ȡ�����ó�ʼ���Ķ������
        /// </summary>
        public string TableName
        {
            get
            {
                return this._tableName;
            }
            set
            {
                this._tableName = value;
            }
        }

        /// <summary>
        /// ���ݿ������ַ�����
        /// </summary>
        protected string _connectionString = string.Empty;

        /// <summary>
        /// ��ȡ�����õ�ǰ�����ݿ������ַ�����
        /// </summary>
        public string ConnectionString
        {
            get { return this._connectionString; }
            set { this._connectionString = value; }
        }

        #endregion

        #region �������ʵ�ֵĺ���(���ڶ�ȡ����)

        /// <summary>
        /// ��DataReader������ֵת��Ϊʵ����������ֵ������ʵ�����
        /// </summary>
        /// <param name="dr">��Ч��DataReader����</param>
        /// <param name="metaDataTable">MetaDataTable����</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <returns>ʵ�����ʵ��</returns>
        protected abstract T DataReaderToEntity(OleDbDataReader dr,MetaDataTable metaDataTable,params string[] columnNames);

        #endregion

        #region IBaseSelect<T> ��Ա

        /// <summary>
        /// ��ѯ���ݿ�,�ж�ָ�������ļ�¼�Ƿ���ڡ�
        /// </summary>
        /// <param name="condition">ָ��������,����Ҫ��SQL����Where�ؼ���,��������ַ����к���SQL�������(@),�ұ���д�����¸�ʽ:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL������Ӧֵ�ļ���,��������ַ����к��в������,��������ø������ֵ</param>
        /// <returns>�����򷵻�<c>true</c>������Ϊ<c>false</c>��</returns>
        public virtual bool IsExistWithCondition(string condition, params object[] parameterValues)
        {
            if (this.ContainWhere(condition))
                throw new ArgumentException("ָ��������,��Ҫ���SQL���Where�ؼ��ֵ�����", "condition");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(0) FROM [{0}] ");
            strSql.Append("WHERE {1} ");
            string sqlCmd = string.Format(strSql.ToString(), this._tableName, condition);
            OleDbParameter[] parameters = this.FillParameters(parameterValues);

            object result = OleDbHelper.ExecuteScalar(this._connectionString, CommandType.Text, sqlCmd, parameters);
            return (Convert.ToInt32(result) > 0 ? true : false);
        }

        /// <summary>
        /// �����ݿ��л�ȡ�������е�ʵ����󼯺ϡ�
        /// </summary>
        /// <returns>�������е�ʵ����󼯺�</returns>
        public virtual List<T> Select()
        {
            return this.SelectWithCondition(string.Empty, null);
        }

        /// <summary>
        /// �����ݿ��л�ȡ���е�ʵ����󼯺�(����ֵ�����ж��Ƿ�Ϊnull)��
        /// </summary>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <returns>ʵ����󼯺�</returns>
        public virtual List<T> Select(params string[] columnNames)
        {
            return this.SelectWithCondition(string.Empty, null, columnNames);
        }

        /// <summary>
        /// �����ݿ��л�ȡ����ָ��������ʵ����󼯺�(����ֵ�����ж��Ƿ�Ϊnull)��
        /// </summary>
        /// <param name="condition">ָ��������,Ҫ���SQL���Where�ؼ��ֵ�����,��������ַ����к���SQL�������(@),�ұ���д�����¸�ʽ:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <returns>ָ�������ı��е�ʵ����󼯺�</returns>
        public virtual List<T> SelectWithCondition(string condition, params string[] columnNames)
        {
            return this.SelectWithCondition(condition, null, columnNames);
        }

        /// <summary>
        /// �����ݿ��л�ȡ���е�ʵ����󼯺�(����ֵ�����ж��Ƿ�Ϊnull)��
        /// </summary>
        /// <param name="orderByColumnName">�����ֶ����ƣ���Ҫ���ORDER BY�ؼ���,ֻҪָ�������ֶ����Ƽ���</param>
        /// <param name="sortType">SQL�����������</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <returns>ʵ����󼯺�</returns>
        public virtual List<T> Select(string orderByColumnName, SortTypeEnum sortType, params string[] columnNames)
        {
            return this.SelectWithCondition(string.Empty, orderByColumnName, sortType, null, columnNames);
        }

        /// <summary>
        /// �����ݿ��л�ȡ����ָ��������ʵ����󼯺�(����ֵ�����ж��Ƿ�Ϊnull)��
        /// </summary>
        /// <param name="condition">ָ��������,Ҫ���SQL���Where�ؼ��ֵ�����,��������ַ����к���SQL�������(@),�ұ���д�����¸�ʽ:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL������Ӧֵ�ļ���,��������ַ����к��в������,��������ø������ֵ</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <returns>ʵ����󼯺�</returns>
        public virtual List<T> SelectWithCondition(string condition, object[] parameterValues, params string[] columnNames)
        {
            return this.SelectWithCondition(condition, string.Empty, SortTypeEnum.ASC, parameterValues, columnNames);
        }

        /// <summary>
        /// �����ݿ��л�ȡ����ָ��������ʵ����󼯺�(����ֵ�����ж��Ƿ�Ϊnull)��
        /// </summary>
        /// <param name="condition">ָ��������,Ҫ���SQL���Where�ؼ��ֵ�����,��������ַ����к���SQL�������(@),�ұ���д�����¸�ʽ:(@p0,@p1...)
        /// <param name="orderByColumnName">�����ֶ����ƣ���Ҫ���ORDER BY�ؼ���,ֻҪָ�������ֶ����Ƽ���</param>
        /// <param name="sortType">SQL�����������</param>
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL������Ӧֵ�ļ���,��������ַ����к��в������,��������ø������ֵ</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <returns>ʵ����󼯺�</returns>
        public virtual List<T> SelectWithCondition(string condition, string orderByColumnName, SortTypeEnum sortType, object[] parameterValues, params string[] columnNames)
        {
            if (!this.ContainWhere(condition))
                throw new ArgumentException("ָ��������,Ҫ���SQL���Where�ؼ��ֵ�����", "condition");

            //��ȡɸѡ��
            string columns = this.GetColumns(columnNames);
            string sqlCmd = string.Format("SELECT {0} FROM [{1}] {2} ", columns, this._tableName, condition);

            if (!string.IsNullOrEmpty(orderByColumnName))
            {
                sqlCmd = string.Format("{0} ORDER BY [{1}] {2}", sqlCmd, orderByColumnName, sortType.ToString());
            }

            return this.GetEntities(sqlCmd, parameterValues, columnNames);
        }

        /// <summary>
        /// �����ݿ��л�ȡ����ָ��������ǰN��ʵ����󼯺�(����ֵ�����ж��Ƿ�Ϊnull)��
        /// </summary>
        /// <param name="topN">���е�ǰN����¼</param>
        /// <param name="condition">ָ��������,Ҫ���SQL���Where�ؼ��ֵ�����,��������ַ����к���SQL�������(@),�ұ���д�����¸�ʽ:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL������Ӧֵ�ļ���,��������ַ����к��в������,��������ø������ֵ</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <returns>ʵ����󼯺�</returns>
        public virtual List<T> SelectTopN(int topN, string condition, object[] parameterValues, params string[] columnNames)
        {
            return this.SelectTopN(topN, condition, string.Empty, SortTypeEnum.ASC, parameterValues, columnNames);
        }

        /// <summary>
        /// �����ݿ��л�ȡ����ָ��������ǰN��ʵ����󼯺�(����ֵ�����ж��Ƿ�Ϊnull)��
        /// </summary>
        /// <param name="topN">���е�ǰN����¼</param>
        /// <param name="condition">ָ��������,Ҫ���SQL���Where�ؼ��ֵ�����,��������ַ����к���SQL�������(@),�ұ���д�����¸�ʽ:(@p0,@p1...)
        /// <param name="orderByColumnName">�����ֶ����ƣ���Ҫ���ORDER BY�ؼ���,ֻҪָ�������ֶ����Ƽ���</param>
        /// <param name="sortType">SQL�����������</param>
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL������Ӧֵ�ļ���,��������ַ����к��в������,��������ø������ֵ</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <returns>ʵ����󼯺�</returns>
        public virtual List<T> SelectTopN(int topN, string condition, string orderByColumnName, SortTypeEnum sortType, object[] parameterValues, params string[] columnNames)
        {
            //���topN��ֵ���Ϸ�.
            if (topN < 1)
            {
                return this.SelectWithCondition(condition, columnNames);
            }

            if (!this.ContainWhere(condition))
                throw new ArgumentException("ָ��������,Ҫ���SQL���Where�ؼ��ֵ�����", "condition");

            //��ȡɸѡ��
            string columns = this.GetColumns(columnNames);
            columns = string.Format("TOP {0} {1}", topN, columns);
            string sqlCmd = string.Format("SELECT {0} FROM [{1}] {2} ", columns, this._tableName, condition);

            if (!string.IsNullOrEmpty(orderByColumnName))
            {
                sqlCmd = string.Format("{0} ORDER BY [{1}] {2}", sqlCmd, orderByColumnName, sortType.ToString());
            }

            return this.GetEntities(sqlCmd, parameterValues, columnNames);
        }

        /// <summary>
        /// �����ݿ��л�ȡһ������ָ��������ʵ����󼯺�(����ֵ��Ҫ�ж��Ƿ�Ϊnull)��
        /// </summary>
        /// <param name="condition">ָ��������,Ҫ���SQL���Where�ؼ��ֵ�����,��������ַ����к���SQL�������(@),�ұ���д�����¸�ʽ:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL������Ӧֵ�ļ���,��������ַ����к��в������,��������ø������ֵ</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <returns>ʵ������null</returns>
        public T SelectOne(string condition, object[] parameterValues, params string[] columnNames)
        {
            List<T> list = this.SelectWithCondition(condition, parameterValues, columnNames);
            return (list == null || list.Count == 0) ? default(T) : list[0];
        }

        /// <summary>
        /// �����ݿ��а���ҳ��С���л�ȡʵ����󼯺�(����ֵ�����ж��Ƿ�Ϊnull),Ĭ�Ϸ��ذ����е��ֶν�������ļ���,
        /// notinColumnName(ָ����ɸѡ������),�ò���Ϊ����ָ������Ϊ��ǰ���кϷ��������ơ����δָ�������ƣ��÷���������null��
        /// </summary>
        /// <param name="pageSize">��ҳ��С������ҳ��ʾ��������¼</param>
        /// <param name="pageIndex">��ǰҳ��</param>
        /// <param name="condition">ָ��������,Ҫ���SQL���Where�ؼ��ֵ�����,�������Where�ؼ�����������Ч,��������ַ����к���SQL�������(@),�ұ���д�����¸�ʽ:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="notinColumnName">ָ����ɸѡ������,�ò���Ϊ����ָ������Ϊ��ǰ���кϷ��������ơ����δָ�������ƣ��÷���������null</param>
        /// <param name="orderByColumnName">�����ֶ����ƣ���Ҫ���ORDER BY�ؼ���,ֻҪָ�������ֶ����Ƽ���</param>
        /// <param name="sortType">SQL�����������</param>
        /// <param name="parameterValues">SQL������Ӧֵ�ļ���,��������ַ����к��в������,��������ø������ֵ</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <returns>��ǰ��ҳ������ȡʵ����󼯺�</returns>
        public virtual PageData<T> SelectWithPageSizeByNotIn(int pageSize, int pageIndex, string condition, string notinColumnName, string orderByColumnName, SortTypeEnum sortType, object[] parameterValues, params string[] columnNames)
        {
            if (string.IsNullOrEmpty(notinColumnName))
                throw new ArgumentException("��ָ����ɸѡ������,�ò���Ϊ����ָ������Ϊ��ǰ���кϷ���������", "notinColumnName");

            if (!this.ContainWhere(condition))
                throw new ArgumentException("ָ��������,Ҫ���SQL���Where�ؼ��ֵ�����", "condition");

            //����Ĭ�Ϸ�ҳ����
            if (pageSize < 1) pageSize = 10;
            if (pageIndex < 0) pageIndex = 0;

            //��ʱ����С
            int tempTableSize = pageIndex * pageSize;

            //��ȡɸѡ��
            string columns = this.GetColumns(columnNames);
            columns = string.Format("TOP {0} {1}", pageSize, columns);

            //���õ������ʽ������Ϊȥ��Where�ַ���
            string param5 = string.IsNullOrEmpty(condition) ?
                string.Empty :
                string.Format("AND {0}", condition.Trim().Substring(5));

            string orderBy = string.IsNullOrEmpty(orderByColumnName) ?
                string.Format("[{0}] {1}", notinColumnName, sortType.ToString()) :
                string.Format("[{0}] {1}", orderByColumnName, sortType.ToString());

            string notInCaluse = tempTableSize > 0 ?
                string.Format("WHERE [{5}] NOT IN (SELECT TOP {0} [{1}] FROM [{2}] {3} ORDER BY {4})",
                tempTableSize, notinColumnName, this._tableName, condition, orderBy, notinColumnName) :
                string.Empty;

            //��ҳ��ȡ���ݿ��¼����SQL���
            StringBuilder sqlFormat = new StringBuilder();
            sqlFormat.Append("SELECT {0} ");
            sqlFormat.Append("FROM [{1}] ");
            sqlFormat.Append("{2} ");
            sqlFormat.Append("{3} ORDER BY {4}");

            string sqlCmd = string.Format(sqlFormat.ToString(), columns, this._tableName, notInCaluse, condition, orderBy);
            return GetPageData(condition, parameterValues, columnNames, sqlCmd);
        }

        /// <summary>
        /// �������ݿ����RowId���Զ����ݽ��з�ҳ��ѯ�ķ���,�÷���ֻ��֧��SqlServer2005���ϰ�����ݿ�(����ֵ�����ж��Ƿ�Ϊnull)��
        /// </summary>
        /// <param name="pageSize">��ҳ��С������ҳ��ʾ��������¼</param>
        /// <param name="pageIndex">��ǰҳ��</param>
        /// <param name="condition">ָ��������,Ҫ���SQL���Where�ؼ��ֵ�����,�������Where�ؼ�����������Ч,��������ַ����к���SQL�������(@),�ұ���д�����¸�ʽ:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="orderByColumnName">�����ֶ����ƣ���Ҫ���ORDER BY�ؼ���,ֻҪָ�������ֶ����Ƽ���</param>
        /// <param name="sortType">SQL�����������</param>
        /// <param name="parameterValues">SQL������Ӧֵ�ļ���,��������ַ����к��в������,��������ø������ֵ</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <returns>��ǰ��ҳ������ȡʵ����󼯺�</returns>
        public virtual PageData<T> SelectWithPageSizeByRowId(int pageSize, int pageIndex, string condition, string orderByColumnName, SortTypeEnum sortType, object[] parameterValues, params string[] columnNames)
        {
            if (string.IsNullOrEmpty(orderByColumnName))
                throw new ArgumentException("�����ֶ����ƣ���Ҫ���ORDER BY�ؼ���,ֻҪָ�������ֶ����Ƽ���", "orderByColumnName");

            if (!this.ContainWhere(condition))
                throw new ArgumentException("ָ��������,Ҫ���SQL���Where�ؼ��ֵ�����", "condition");
            
            //����Ĭ�Ϸ�ҳ����
            if (pageSize < 1) pageSize = 10;
            if (pageIndex < 0) pageIndex = 0;

            //���÷�ҳ������յ�
            int startRowId = (pageIndex - 1) * pageSize + 1;
            int endRowId = pageSize * pageIndex;
            endRowId = endRowId == 0 ? pageSize : endRowId;

            //��ȡɸѡ��
            string columns = this.GetColumns(columnNames);

            //��ҳ��ȡ���ݿ��¼����SQL���
            StringBuilder sqlFormat = new StringBuilder();
            sqlFormat.Append("SELECT *  ");
            sqlFormat.Append("FROM  (SELECT {0}, ");
            sqlFormat.Append("ROW_NUMBER() OVER (ORDER BY {1} {2}) AS 'RowId' ");
            sqlFormat.Append("FROM [{3}] {4}) as temptable ");
            sqlFormat.Append("WHERE RowId BETWEEN {5} AND {6} ");

            string sqlCmd = string.Format(sqlFormat.ToString(), columns, orderByColumnName, sortType.ToString(), this._tableName, condition, startRowId, endRowId);
            return GetPageData(condition, parameterValues, columnNames, sqlCmd);
        }

        /// <summary>
        /// ���ô洢���̶Ա������ݽ��з�ҳ��ѯ�ķ���(����ֵ�����ж��Ƿ�Ϊnull)��
        /// </summary>
        /// <param name="pageSize">��ҳ��С������ҳ��ʾ��������¼</param>
        /// <param name="pageIndex">��ǰҳ��</param>
        /// <param name="condition">ָ��������,Ҫ�󲻴�SQL���Where�ؼ��ֵ�����,��ֵΪ������</param>
        /// <param name="orderby">�����ֶ����ƣ���Ҫ���ORDER BY�ؼ���,ֻҪָ�������ֶ����Ƽ���</param>
        /// <param name="sortType">SQL�����������</param>
        /// <param name="parameterValues">SQL������Ӧֵ�ļ���,��������ַ����к��в������,��������ø������ֵ</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <returns>��ǰ��ҳ������ȡʵ����󼯺�</returns>
        public virtual PageData<T> SelectWithPageSizeByStoredProcedure(int pageSize, int pageIndex, string condition, string orderby, SortTypeEnum sortType, params string[] columnNames)
        {
            if (this.ContainWhere(condition))
                throw new ArgumentException("ָ��������,Ҫ�󲻴�SQL���Where�ؼ��ֵ�����", "condition");

            OleDbParameter[] parameters = {
                                            new OleDbParameter("@tblName",OleDbType.VarChar, 255),
                                            new OleDbParameter("@fldName", OleDbType.VarChar, 255),
                                            new OleDbParameter("@OrderfldName", OleDbType.VarChar, 255),
                                            new OleDbParameter("@StatfldName", OleDbType.VarChar, 255),
                                            new OleDbParameter("@PageSize", OleDbType.Integer),
                                            new OleDbParameter("@PageIndex", OleDbType.Integer),
                                            new OleDbParameter("@IsReCount", OleDbType.Integer),
                                            new OleDbParameter("@OrderType", OleDbType.Integer),
                                            new OleDbParameter("@strWhere", OleDbType.VarChar,1000)
                                        };
            parameters[0].Value = this._tableName;
            parameters[1].Value = "*";
            parameters[2].Value = orderby;
            parameters[3].Value = "";
            parameters[4].Value = pageSize;
            parameters[5].Value = pageIndex;
            parameters[6].Value = 0;
            parameters[7].Value = (int)sortType;
            parameters[8].Value = condition;

            PageData<T> pageData = new PageData<T>();
            pageData.RecordSet = this.GetEntities("sp_SelectWithPageSize", parameters, CommandType.StoredProcedure, columnNames);
            pageData.Count = this.Count(condition);
            return pageData;

            //�洢���̴���
            //CREATE PROCEDURE [dbo].[sp_SelectWithPageSize]
            //    @tblName      varchar(255),       -- ����
            //    @fldName      varchar(255),       -- �ֶ���
            //    @PageSize     int = 10,           -- ҳ�ߴ�
            //    @PageIndex    int = 1,            -- ҳ��
            //    @IsReCount    bit = 0,            -- ���ؼ�¼����, �� 0 ֵ�򷵻�
            //    @OrderType    bit = 0,            -- ������������, �� 0 ֵ����
            //    @strWhere     varchar(1000) = ''  -- ��ѯ���� (ע��: ��Ҫ�� where)
            //AS

            //declare @strSQL   varchar(6000)       -- �����
            //declare @strTmp   varchar(100)        -- ��ʱ����
            //declare @strOrder varchar(400)        -- ��������

            //if @OrderType != 0
            //begin
            //    set @strTmp = '<(select min'
            //    set @strOrder = ' order by [' + @fldName +'] desc'
            //end
            //else
            //begin
            //    set @strTmp = '>(select max'
            //    set @strOrder = ' order by [' + @fldName +'] asc'
            //end

            //set @strSQL = 'select top ' + str(@PageSize) + ' * from ['
            //    + @tblName + '] where [' + @fldName + ']' + @strTmp + '(['
            //    + @fldName + ']) from (select top ' + str((@PageIndex-1)*@PageSize) + ' ['
            //    + @fldName + '] from [' + @tblName + ']' + @strOrder + ') as tblTmp)'
            //    + @strOrder

            //if @strWhere != ''
            //    set @strSQL = 'select top ' + str(@PageSize) + ' * from ['
            //        + @tblName + '] where [' + @fldName + ']' + @strTmp + '(['
            //        + @fldName + ']) from (select top ' + str((@PageIndex-1)*@PageSize) + ' ['
            //        + @fldName + '] from [' + @tblName + '] where ' + @strWhere + ' '
            //        + @strOrder + ') as tblTmp) and ' + @strWhere + ' ' + @strOrder

            //if @PageIndex = 1
            //begin
            //    set @strTmp =''
            //    if @strWhere != ''
            //        set @strTmp = ' where ' + @strWhere

            //    set @strSQL = 'select top ' + str(@PageSize) + ' * from ['
            //        + @tblName + ']' + @strTmp + ' ' + @strOrder
            //end

            //if @IsReCount != 0
            //    set @strSQL = 'select count(*) as Total from [' + @tblName + ']'+' where ' + @strWhere

            //exec (@strSQL)
        }

        /// <summary>
        /// ��ȡ���ݿ��б��ļ�¼������
        /// </summary>
        /// <returns>���ļ�¼����</returns>
        public virtual int Count()
        {
            return this.Count(string.Empty);
        }

        /// <summary>
        /// ��ȡ���ݿ����ָ�������ļ�¼������
        /// </summary>
        /// <param name="condition">Ҫ���SQL���Where�ؼ��ֵ��������������Where�ؼ��ָ÷������Ա������м�¼ִ�в���,��������ַ����к���SQL�������(@),�ұ���д�����¸�ʽ:(@p0,@p1...)
        /// <example>e.g.:[UserName]=@p0 AND [Password] = @p1</example></param>
        /// <param name="parameterValues">SQL������Ӧֵ�ļ���,��������ַ����к��в������,��������ø������ֵ</param>
        /// <returns>ָ�������ļ�¼����</returns>
        public virtual int Count(string condition, params object[] parameterValues)
        {
            if (!this.ContainWhere(condition))
                throw new ArgumentException("ָ��������,Ҫ���SQL���Where�ؼ��ֵ�����", "condition");

            StringBuilder strSql = new StringBuilder();
            strSql.Append("SELECT COUNT(0) AS TotalCount FROM [{0}] {1}");

            string sqlCmd = sqlCmd = string.Format(strSql.ToString(), this._tableName, condition);
            OleDbParameter[] parameters = this.FillParameters(parameterValues);
            object obj = OleDbHelper.ExecuteScalar(this._connectionString, CommandType.Text, sqlCmd, parameters);
            return Convert.IsDBNull(obj) ? 0 : Convert.ToInt32(obj);
        }

        #endregion

        #region �෽����Ա

        /// <summary>
        /// �������Ƿ���Where�ؼ���
        /// </summary>
        /// <param name="condition">����</param>
        /// <returns>true|false</returns>
        protected bool ContainWhere(string condition)
        {
            return (string.IsNullOrEmpty(condition) ||
                (!string.IsNullOrEmpty(condition) && 
                condition.IndexOf("where", StringComparison.CurrentCultureIgnoreCase) != -1));
        }

        /// <summary>
        /// ִ��һ����ѯ��䣬����ʵ����󼯺���ʽ���ز�ѯ�������
        /// </summary>
        /// <param name="sqlCmd">SQL����</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <returns>ʵ����󼯺�</returns>
        protected List<T> GetEntities(string sqlCmd,params string[] columnNames)
        {
            return this.GetEntities(sqlCmd, null, columnNames);
        }

        /// <summary>
        /// ִ��һ����ѯ��䣬����ʵ����󼯺���ʽ���ز�ѯ�������
        /// </summary>
        /// <param name="sqlCmd">SQL����</param>
        /// <param name="parameterValues">SQL������Ӧֵ�ļ���,���SQL����ַ����к��в������,��������ø������ֵ</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <returns>ʵ����󼯺�</returns>
        protected List<T> GetEntities(string sqlCmd, object[] parameterValues, params string[] columnNames)
        {
            return this.GetEntities(sqlCmd, parameterValues, CommandType.Text, columnNames);
        }

        /// <summary>
        /// ִ��һ����ѯ��䣬����ʵ����󼯺���ʽ���ز�ѯ�������
        /// </summary>
        /// <param name="sqlCmd">SQL����</param>
        /// <param name="parameterValues">SQL������Ӧֵ�ļ���,���SQL����ַ����к��в������,��������ø������ֵ</param>
        /// <param name="commandType">CommandType</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <returns>ʵ����󼯺�</returns>
        protected List<T> GetEntities(string sqlCmd, object[] parameterValues, CommandType commandType, params string[] columnNames)
        {
            return this.GetEntities(sqlCmd, this.FillParameters(parameterValues), commandType, columnNames);
        }

        /// <summary>
        /// ִ��һ����ѯ��䣬����ʵ����󼯺���ʽ���ز�ѯ�������
        /// </summary>
        /// <param name="sqlCmd">SQL����</param>
        /// <param name="parameters">SQL������Ӧֵ�ļ���,���SQL����ַ����к��в������,��������ø������ֵ</param>
        /// <param name="commandType">CommandType</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <returns>ʵ����󼯺�</returns>
        protected virtual List<T> GetEntities(string sqlCmd, OleDbParameter[] parameters, CommandType commandType, params string[] columnNames)
        {
            return this.GetEntities<T>(sqlCmd, parameters, commandType, this.DataReaderToEntity, columnNames);
        }

        /// <summary>
        /// ִ��һ����ѯ��䣬����ʵ����󼯺���ʽ���ز�ѯ�������
        /// </summary>
        /// <typeparam name="TEntity">ʵ������</typeparam>
        /// <param name="sqlCmd">SQL����</param>
        /// <param name="parameters">SQL������Ӧֵ�ļ���,���SQL����ַ����к��в������,��������ø������ֵ</param>
        /// <param name="commandType">CommandType</param>
        /// <param name="drToEntityAction">����DataReader���ݵ�DTO</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <returns>ʵ����󼯺�</returns>
        protected virtual List<TEntity> GetEntities<TEntity>(string sqlCmd, OleDbParameter[] parameters, CommandType commandType,
            Func<OleDbDataReader,MetaDataTable,string[], TEntity> drToEntityAction, params string[] columnNames)
        {
            List<TEntity> entities = new List<TEntity>();
            var metaDataTable = new MetaDataTable(typeof(TEntity), this._tableName);

            using (OleDbDataReader dr = OleDbHelper.ExecuteReader(this._connectionString, commandType, sqlCmd, parameters))
            {
                try
                {
                    while (dr.Read())
                    {
                        entities.Add(drToEntityAction(dr, metaDataTable, columnNames));
                    }
                }
                catch (Exception ex)
                {
                    string message = string.Format("[SQL]:{0},[Exception]:{1}", sqlCmd, ex.ToString());
                    System.Diagnostics.EventLog.WriteEntry("LightFramework.Data.OleDb", message);
                }
            }

            return entities;
        }

        /// <summary>
        /// ��ȡ��ҳ���ݼ��ϡ�
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <param name="parameterValues">SQL������Ӧֵ�ļ���,���SQL����ַ����к��в������,��������ø������ֵ</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <param name="sqlCmd">SQL����</param>
        /// <returns>��ҳ���ݼ���</returns>
        protected PageData<T> GetPageData(string condition, object[] parameterValues, string[] columnNames, string sqlCmd)
        {
            return this.GetPageData(condition, parameterValues, columnNames, sqlCmd, CommandType.Text);
        }

        /// <summary>
        /// ��ȡ��ҳ���ݼ��ϡ�
        /// </summary>
        /// <param name="condition">��ѯ����</param>
        /// <param name="parameterValues">SQL������Ӧֵ�ļ���,���SQL����ַ����к��в������,��������ø������ֵ</param>
        /// <param name="columnNames">��ʵ������ж�Ӧ�����ݿ��������</param>
        /// <param name="sqlCmd">SQL����</param>
        /// <param name="commandType">CommandType</param>
        /// <returns>��ҳ���ݼ���</returns>
        protected virtual PageData<T> GetPageData(string condition, object[] parameterValues, string[] columnNames, string sqlCmd, CommandType commandType)
        {
            PageData<T> pageData = new PageData<T>();
            pageData.RecordSet = this.GetEntities(sqlCmd, parameterValues, commandType, columnNames);
            pageData.Count = this.Count(condition, parameterValues);
            return pageData;
        }

        ///<summary>
        ///���ɲ������󼯺ϡ�
        /// </summary>
        /// <param name="parameterValues">SQL����ֵ�ļ���</param>
        /// <returns>�������󼯺�</returns>
        protected virtual OleDbParameter[] FillParameters(object[] parameterValues)
        {
            if (parameterValues == null || 
                parameterValues.Length == 0)
            {
                return null;
            }

            OleDbParameter[] parameters = new OleDbParameter[parameterValues.Length];
            for (int i = 0; i < parameterValues.Length; i++)
            {
                parameters[i] = new OleDbParameter();
                parameters[i].ParameterName = "@p" + i;
                parameters[i].Value = parameterValues[i] != null ? parameterValues[i] : DBNull.Value;
            }

            return parameters;
        }

        /// <summary>
        /// ��ȡɸѡ��������ɵ��ַ�����
        /// </summary>
        /// <param name="columnNames">ɸѡ����������</param>
        /// <returns>������ɵ��ַ���</returns>
        protected virtual string GetColumns(params string[] columnNames)
        {
            if (columnNames == null ||
                columnNames.Length == 0) return "*";

            string[] cols = new string[columnNames.Length];
            for (int i = 0; i < columnNames.Length; i++)
            {
                cols[i] = string.Format("[{0}]", columnNames[i]);
            }

            return string.Join(",", cols);
        }

        /// <summary>
        /// ��ȡSQL������Ӧ��ֵ�ļ��ϡ�
        /// </summary>
        /// <param name="values">ֵ</param>
        /// <returns>SQL������Ӧ��ֵ�ļ���</returns>
        protected virtual object[] GetParamerterValues(params object[] values)
        {
            return values;
        }

        /// <summary>
        /// �ѵ�ǰSql���������ʽִ��,����ReadCommitted���뼶��
        /// </summary>
        /// <param name="sqlExpression">SqlExpression����</param>
        protected void ExecuteByTransaction(SqlExpression sqlExpression)
        {
            this.ExecuteByTransaction(sqlExpression, IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// �ѵ�ǰSql���������ʽִ�С�
        /// </summary>
        /// <param name="sqlExpressions">SqlExpression����</param>
        /// <param name="isolationLevel">������뼶��</param>
        protected void ExecuteByTransaction(SqlExpression sqlExpression, IsolationLevel isolationLevel)
        {
            this.ExecuteByTransaction(new List<SqlExpression>(1) { sqlExpression }, isolationLevel);
        }

        /// <summary>
        /// �ѵ�ǰSql���������ʽִ�С�
        /// </summary>
        /// <param name="sqlExpressions">SqlExpression���󼯺�</param>
        protected void ExecuteByTransaction(List<SqlExpression> sqlExpressions)
        {
            this.ExecuteByTransaction(sqlExpressions,IsolationLevel.ReadCommitted);
        }

        /// <summary>
        /// �ѵ�ǰSql���������ʽִ�С�
        /// </summary>
        /// <param name="sqlExpressions">SqlExpression���󼯺�</param>
        /// <param name="isolationLevel">������뼶��</param>
        protected virtual void ExecuteByTransaction(List<SqlExpression> sqlExpressions, IsolationLevel isolationLevel)
        {
            if (sqlExpressions == null ||
                sqlExpressions.Count == 0) throw new ArgumentNullException("sqlExpressions");

            using (OleDbConnection oleDbConnection = new OleDbConnection(this._connectionString))
            {
                oleDbConnection.Open();
                using (OleDbTransaction oleDbTransaction = oleDbConnection.BeginTransaction(isolationLevel))
                {
                    try
                    {
                        sqlExpressions.ForEach(expr =>
                            OleDbHelper.ExecuteNonQuery(oleDbTransaction, CommandType.Text, expr.CommandText, expr.Parameters));
                        oleDbTransaction.Commit();
                    }
                    catch (OleDbException ex)
                    {
                        System.Diagnostics.EventLog.WriteEntry("LightFramework.Data.OleDb", ex.ToString());
                    }
                }
            }
        }

        #endregion
    }
}