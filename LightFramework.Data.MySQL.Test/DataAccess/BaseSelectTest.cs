using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;
using NUnit.Framework;

namespace LightFramework.Data.MySQL.Test.DataAccess
{
    using Data;
    using Data.Test.DTO;

    public class BaseSelectTest
    {
        protected Category _category;

        protected void Setup()
        {
            this._category = new Category(ConfigurationManager.ConnectionStrings["lightframework"].ConnectionString);
        }

        protected void Dispose()
        {
            this._category = null;
        }

        public void IsNotNullOfCategory()
        {
            Assert.That(this._category, Is.Not.Null);
        }

        public void TableNameIsCorrect()
        {
            Assert.That(this._category.TableName == CategoryDTO.ENTITYNAME);
        }

        public void ConnectionStringIsCorrect()
        {
            string connstr = ConfigurationManager.ConnectionStrings["lightframework"].ConnectionString;
            Assert.That(this._category.ConnectionString == connstr);
        }
    }
}
