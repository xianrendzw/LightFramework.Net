using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace LightFramework.Data.Test.Criterion
{
    using Data;

    public class RestrictionsTest
    {
        [Test]
        [Category("LightFramework.Data")]
        public void EqualOperand()
        {
            Assert.That(new EqualOperand("columnName", "'columnValue'").ToString(),
                Is.EqualTo("columnName = 'columnValue' "));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void BetweenOperand()
        {
            string expr = "columnName BETWEEN columnValue1 AND columnValue2 ";
            Assert.That(new BetweenOperand("columnName", "columnValue1", "columnValue2").ToString(), Is.EqualTo(expr));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void GreaterThanOperand()
        {
            string expr = "columnName > columnValue ";
            Assert.That(new GreaterThanOperand("columnName", "columnValue").ToString(), Is.EqualTo(expr));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void GreaterThanOrEqualOperand()
        {
            string expr = "columnName >= columnValue ";
            Assert.That(new GreaterThanOrEqualOperand("columnName", "columnValue").ToString(), Is.EqualTo(expr));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void InOperand()
        {
            string expr = "columnName IN ('v1','v2','v3') ";
            Assert.That(new InOperand("columnName", "'v1','v2','v3'").ToString(), Is.EqualTo(expr));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void LessThanOperand()
        {
            string expr = "columnName < columnValue ";
            Assert.That(new LessThanOperand("columnName", "columnValue").ToString(), Is.EqualTo(expr));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void LessThanOrEqualOperand()
        {
            string expr = "columnName <= columnValue ";
            Assert.That(new LessThanOrEqualOperand("columnName", "columnValue").ToString(), Is.EqualTo(expr));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void LikeOperand()
        {
            string expr = "columnName like '%columnValue%' ";
            Assert.That(new LikeOperand("columnName", "%columnValue%").ToString(), Is.EqualTo(expr));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void NotEqualOperand()
        {
            string expr = "columnName <> columnValue ";
            Assert.That(new NotEqualOperand("columnName", "columnValue").ToString(), Is.EqualTo(expr));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void NotInOperand()
        {
            string expr = "columnName NOT IN ('v1','v2','v3') ";
            Assert.That(new NotInOperand("columnName", "'v1','v2','v3'").ToString(), Is.EqualTo(expr));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void NotLikeOperand()
        {
            string expr = "columnName NOT LIKE '%columnValue%' ";
            Assert.That(new NotLikeOperand("columnName", "%columnValue%").ToString(), Is.EqualTo(expr));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void CompositeOperand()
        {
            string sqlCondition = "Where Name = TomDeng  AND Age = 29  AND (Weight BETWEEN 100 AND 180  AND Salary < 20w )";

            Operand operand = Restrictions.Clause(SqlClause.Where)
               .Append(Restrictions.Equal("Name", "TomDeng"))
               .Append(Restrictions.And)
               .Append(Restrictions.Equal("Age", 29))
               .Append(Restrictions.And)
               .Append(Restrictions.Bracket(Bracket.Left))
               .Append(Restrictions.Between("Weight", 100, 180))
               .Append(Restrictions.And)
               .Append(Restrictions.LessThan("Salary", "20w"))
               .Append(Restrictions.Bracket(Bracket.Rgiht));

            Assert.That(operand.ToString(), Is.EqualTo(sqlCondition));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void NestedCompositeOperand()
        {
            string sqlCondition = "Where Name = TomDeng  AND Age = 29  AND (Weight BETWEEN 100 AND 180  AND Salary < 20w )";

            Operand operand = Restrictions.Clause(SqlClause.Where)
               .Append(Restrictions.Equal("Name", "TomDeng"))
               .Append(Restrictions.And)
               .Append(Restrictions.Equal("Age", 29)
                   .Append(Restrictions.And)
                   .Append(Restrictions.Bracket(Bracket.Left))
                   .Append(Restrictions.Between("Weight", 100, 180))
                   .Append(Restrictions.And)
                   .Append(Restrictions.LessThan("Salary", "20w"))
                   .Append(Restrictions.Bracket(Bracket.Rgiht))
               );

            Assert.That(operand.ToString(), Is.EqualTo(sqlCondition));
        }
    }
}
