using System;
using System.Collections.Generic;
using System.Text;

namespace LightFramework.Data
{
    /// <summary>
    /// 构造SQL语句的条件类
    /// </summary>
    public static class Restrictions
    {
        public static NoneOperand None
        {
            get { return new NoneOperand(); }
        }

        public static AndConjOperand And
        {
            get { return new AndConjOperand(); }
        }

        public static OrConjOperand Or
        {
            get { return new OrConjOperand(); }
        }

        public static BetweenOperand Between(string columnName, object lowerValue, object higherValue)
        {
            return new BetweenOperand(columnName, lowerValue, higherValue);
        }

        public static BracketOperand Bracket(Bracket bracket)
        {
            return new BracketOperand(bracket);
        }

        public static EqualOperand Equal(string columnName, object columnValue)
        {
            return new EqualOperand(columnName, columnValue);
        }

        public static EqualOperand Equal(string columnName, string columnValue)
        {
            return new EqualOperand(columnName, String.Format("'{0}'", columnValue));
        }

        public static ClauseOperand Clause(SqlClause sqlClause)
        {
            return new ClauseOperand(sqlClause);
        }

        public static GreaterThanOperand GreaterThan(string columnName, object columnValue)
        {
            return new GreaterThanOperand(columnName, columnValue);
        }

        public static GreaterThanOrEqualOperand GreaterThanOrEqual(string columnName, object columnValue)
        {
            return new GreaterThanOrEqualOperand(columnName, columnValue);
        }

        public static InOperand In(string columnName, object columnValue)
        {
            return new InOperand(columnName, columnValue);
        }

        public static LessThanOperand LessThan(string columnName, object columnValue)
        {
            return new LessThanOperand(columnName, columnValue);
        }

        public static LessThanOrEqualOperand LessThanOrEqual(string columnName, object columnValue)
        {
            return new LessThanOrEqualOperand(columnName, columnValue);
        }

        public static LikeOperand Like(string columnName, object columnValue)
        {
            return new LikeOperand(columnName, columnValue);
        }

        public static NotEqualOperand NotEqual(string columnName, object columnValue)
        {
            return new NotEqualOperand(columnName, columnValue);
        }

        public static NotInOperand NotIn(string columnName, object columnValue)
        {
            return new NotInOperand(columnName, columnValue);
        }

        public static NotLikeOperand NotLike(string columnName, object columnValue)
        {
            return new NotLikeOperand(columnName, columnValue);
        }

        public static OrderByOperand OrderBy(SortTypeEnum sortType, params string[] columnNames)
        {
            return new OrderByOperand(sortType, columnNames);
        }

        public static GroupByOperand GroupBy(params string[] columnNames)
        {
            return new GroupByOperand(columnNames);
        }
    }

}