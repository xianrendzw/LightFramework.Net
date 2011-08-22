using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using NUnit.Framework;

namespace LightFramework.Data.Test.Mapping
{
    using DTO;
    using Data;

    public class MetaDataColumnTest
    {
        protected MetaDataColumn _metaDataColumn;

        [SetUp]
        protected void Setup()
        {
            CategoryDTO category = new CategoryDTO();
            
            PropertyInfo propertyInfo = category.GetType().GetProperty("Name");
            if (propertyInfo == null) return;
            var attr = (ColumnAttribute)Attribute.GetCustomAttribute(propertyInfo, typeof(ColumnAttribute));
            if(attr == null) return;

            this._metaDataColumn =  new MetaDataColumn(propertyInfo,attr);
        }

        [TearDown]
        protected void Dispose()
        {
            this._metaDataColumn = null;
        }

        [Test]
        [Category("LightFramework.Data")]
        public void Is_Not_Null_Of_MemaDataColumn()
        {
            Assert.That(this._metaDataColumn, Is.Not.Null);
        }

        [Test]
        [Category("LightFramework.Data")]
        public void Attribute_Should_Return_Correct_Result()
        {
            Assert.That(this._metaDataColumn.Attribute.Name, Is.EqualTo("Name"));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void DataType_Should_Return_Correct_Result()
        {
            Assert.That(this._metaDataColumn.DataType, Is.EqualTo(typeof(String)));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void Name_Should_Return_Correct_Result()
        {
            Assert.That(Is.Equals(this._metaDataColumn.Name, "Name"));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void Member_Should_Return_Correct_Result()
        {
            Assert.That(this._metaDataColumn.Member.Name, Is.EqualTo("Name"));
        }
    }
}
