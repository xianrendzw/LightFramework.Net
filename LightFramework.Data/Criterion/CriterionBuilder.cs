using System;
using System.Collections.Generic;
using System.Text;

namespace LightFramework.Data
{
    /// <summary>
    /// 构造SQL语句的工具类
    /// </summary>
    public static class CriterionBuilder
    {
        /// <summary>
        /// 添加查询条件 Where:返回" WHERE 1=1 ";其他:返回 " 1=1 "
        /// </summary>
        /// <returns>Where:返回" WHERE 1=1 ";其他:返回 " 1=1 "</returns>
        public static string Build(SqlClause condition)
        {
            return (condition == SqlClause.Where) ? " WHERE 1 = 1 " : " 1 = 1 ";
        }

        /// <summary>
        /// 添加查询条件 = 返回 " AND [columnName] = 'columnValue' "
        /// </summary>
        /// <param name="columnName">列名</param>
        /// <param name="columnValue">查询值</param>
        /// <returns>" AND [columnName] = 'columnValue' "</returns>
        public static string Build(string columnName, object columnValue)
        {
            return new EqualOperand(columnName, columnValue).ToString();
        }

        public static string Build(Operand operand)
        {
            return string.Empty;
        }

        //public static string Build(string columnName, object columnValue, Operand operand)
        //{
        //    string value = columnValue.ToString();
        //    value = value.Replace("'", "''").Trim();
        //    return operand.BuilderCondition(columnName, value);
        //}
    }

}