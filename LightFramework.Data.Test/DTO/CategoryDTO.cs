﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data.Test.DTO
{
    using Data;

    public class CategoryDTO
    {
        #region Const Members

        /// <summary>
        /// 实体Category名称
        /// </summary>
        public static string ENTITYNAME = "Category";

        /// <summary>
        /// 列名Id,Id
        /// </summary>
        public static readonly string C_Id = "Id";

        /// <summary>
        /// 列名Name,Name
        /// </summary>
        public static readonly string C_Name = "Name";

        /// <summary>
        /// 列名Description,Description
        /// </summary>
        public static readonly string C_Description = "Description";



        #endregion

        #region Field Members

        private Int32 _id;

        private String _name;

        private String _description;



        #endregion

        #region Property Members

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        [Column(Name = "Id")]
        public virtual Int32 Id
        {
            get
            {
                return this._id;
            }
            set
            {
                this._id = value;
            }
        }

        /// <summary>
        /// 获取或设置Name
        /// </summary>
        [Column(Name = "Name")]
        public virtual String Name
        {
            get
            {
                return this._name ?? String.Empty;
            }
            set
            {
                this._name = value;
            }
        }

        /// <summary>
        /// 获取或设置Description
        /// </summary>
        [Column(Name = "Description")]
        public virtual String Description
        {
            get
            {
                return this._description ?? String.Empty;
            }
            set
            {
                this._description = value;
            }
        }

        #endregion
    }
}
