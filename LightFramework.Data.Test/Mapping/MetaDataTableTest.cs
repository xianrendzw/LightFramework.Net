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

    public class MetaDataTableTest
    {
        protected MetaDataTable _metaDataTable;

        [SetUp]
        protected void Setup()
        {
            CategoryDTO category = new CategoryDTO();
            this._metaDataTable = new MetaDataTable(category.GetType(), CategoryDTO.ENTITYNAME);
        }

        [TearDown]
        protected void Dispose()
        {
            this._metaDataTable = null;
        }

        [Test]
        [Category("LightFramework.Data")]
        public void Is_Not_Null_Of_MetaDataTable()
        {
            Assert.That(this._metaDataTable, Is.Not.Null);
        }

        [Test]
        [Category("LightFramework.Data")]
        public void TableName_Should_Return_Correct_Result()
        {
            Assert.That(Is.Equals(this._metaDataTable.TableName, CategoryDTO.ENTITYNAME));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void EntityType_Should_Return_Correct_Result()
        {
            string typeName = this._metaDataTable.EntityType.Name;
            Assert.That(typeName, Is.EqualTo(typeof(CategoryDTO).Name));
        }

        [Test]
        [Category("LightFramework.Data")]
        public void Columns_Should_Return_Correct_Result()
        {
            PropertyInfo[] properties = this._metaDataTable.EntityType.GetProperties();
            Assert.That(this._metaDataTable.Columns.Keys.ToArray(),
                Is.All.EqualTo(properties.Select(x => x.Name.ToLower()).ToArray()));
            Assert.That(this._metaDataTable.Columns.Values.Select(x => x.Member).ToArray(),
                Is.All.EqualTo(properties));
        }
    }
}
