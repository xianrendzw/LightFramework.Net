using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MySql.Data.MySqlClient;

namespace LightFramework.Data.MySQL.Test.DataAccess
{
    using Data;
    using Data.MySQL;
    using Data.Test.DTO;

    public class Category : SinglePKDataAccess<CategoryDTO>
    {
        public Category(string connectionString)
            : base(CategoryDTO.ENTITYNAME, CategoryDTO.C_Id, connectionString)
        {
        }

        public Category(string tableName, string connectionString)
            : base(tableName, CategoryDTO.C_Id, connectionString)
        {
        }

        public Category(string tableName, string primaryKey, string connectionString)
            : base(tableName, primaryKey, connectionString)
        {
        }

        #region

        protected override CategoryDTO DataReaderToEntity(MySqlDataReader dr, MetaDataTable metaDataTable, params string[] columnNames)
        {
            if (dr == null)
                throw new ArgumentNullException("dr", "未将对象引用到实例");

            return EntityMapper.GetEntity(dr, new CategoryDTO(), metaDataTable);
        }

        protected override DataFieldMapTable GetDataFieldMapTable(CategoryDTO dto, params string[] columnNames)
        {
            if (dto == null)
                throw new ArgumentNullException("dto", "未将对象引用到实例");

            return EntityMapper.GetMapTable(dto, columnNames);
        }

        #endregion

        #region

        #endregion
    }
}
