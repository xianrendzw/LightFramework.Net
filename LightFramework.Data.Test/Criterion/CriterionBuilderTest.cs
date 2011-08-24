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
            Assert.That(CriterionBuilder.Build(SqlClause.Empty)
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void EqualOperand()
        {
            string expr = " AND [columnName] = 'columnValue' ";
            Assert.That(CriterionBuilder.Build("columnName", "columnValue")
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void ReplaceSingleQuotesOfColumnValue()
        {
            string expr = " AND [columnName] = 'column''Value' ";
            Assert.That(CriterionBuilder.Build("columnName", "column'Value")
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void BetweenOperand()
        {
            string expr = " AND [columnName] = 'columnValue' ";
            Assert.That(CriterionBuilder.Build("columnName", "columnValue",)
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void GreaterThanOperand()
        {
            string expr = " AND [columnName] = 'columnValue' ";
            Assert.That(CriterionBuilder.Build("columnName", "columnValue")
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void GreaterThanOrEqualOperand()
        {
            string expr = " AND [columnName] = 'columnValue' ";
            Assert.That(CriterionBuilder.Build("columnName", "columnValue")
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void InOperand()
        {
            string expr = " AND [columnName] = 'columnValue' ";
            Assert.That(CriterionBuilder.Build("columnName", "columnValue")
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void LessThanOperand()
        {
            string expr = " AND [columnName] = 'columnValue' ";
            Assert.That(CriterionBuilder.Build("columnName", "columnValue")
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void LessThanOrEqualOperand()
        {
            string expr = " AND [columnName] = 'columnValue' ";
            Assert.That(CriterionBuilder.Build("columnName", "columnValue")
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void LikeOperand()
        {
            string expr = " AND [columnName] = 'columnValue' ";
            Assert.That(CriterionBuilder.Build("columnName", "columnValue")
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void NotEqualOperand()
        {
            string expr = " AND [columnName] = 'columnValue' ";
            Assert.That(CriterionBuilder.Build("columnName", "columnValue")
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }

        public void NotInOperand()
        {
            string expr = " AND [columnName] = 'columnValue' ";
            Assert.That(CriterionBuilder.Build("columnName", "columnValue")
                .Equals(expr, StringComparison.CurrentCultureIgnoreCase));
        }
    }
}
