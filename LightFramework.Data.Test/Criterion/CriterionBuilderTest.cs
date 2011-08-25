using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace LightFramework.Data.Test.Criterion
{
    using Data;

    public class CriterionBuilderTest
    {
        [Test]
        [Category("LightFramework.Data")]
        public void WhereCondition()
        {
            string expr = " WHERE 1 = 1 ";
            Assert.That(CriterionBuilder.Build(SqlClause.Where)
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void NoWhereCondition()
        {
            string expr = "1 = 1 ";
            Assert.That(CriterionBuilder.Build(SqlClause.None)
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void EqualOperand()
        {
            string expr = "[columnName] = 'columnValue' ";
            Assert.That(new EqualOperand("columnName", "columnValue").ToString()
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void BetweenOperand()
        {
            string expr = "columnName BETWEEN columnValue1 AND columnValue2  ";
            Assert.That(new BetweenOperand("columnName", "columnValue1","columnValue2").ToString()
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void GreaterThanOperand()
        {
            string expr = "columnName > columnValue   ";
            Assert.That(new GreaterThanOperand("columnName", "columnValue").ToString()
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void GreaterThanOrEqualOperand()
        {
            string expr = "[columnName] = 'columnValue' ";
            Assert.That(new EqualOperand("columnName", "columnValue").ToString()
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void InOperand()
        {
            string expr = " AND [columnName] = 'columnValue' ";
            Assert.That(new EqualOperand("columnName", "columnValue").ToString()
                 .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void LessThanOperand()
        {
            string expr = "[columnName] = 'columnValue' ";
            Assert.That(new EqualOperand("columnName", "columnValue").ToString()
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void LessThanOrEqualOperand()
        {
            string expr = "columnName] = 'columnValue' ";
            Assert.That(new EqualOperand("columnName", "columnValue").ToString()
                 .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void LikeOperand()
        {
            string expr = "[columnName] = 'columnValue' ";
            Assert.That(new EqualOperand("columnName", "columnValue").ToString()
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void NotEqualOperand()
        {
            string expr = "[columnName] = 'columnValue' ";
            Assert.That(new EqualOperand("columnName", "columnValue").ToString()
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void NotInOperand()
        {
            string expr = "[columnName] = 'columnValue' ";
            Assert.That(new NotInOperand("columnName", "columnValue").ToString()
                 .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
