using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data.Test.DTO
{
    public class AccountDTO
    {
        #region Const Members

        /// <summary>
        /// 实体Account名称
        /// </summary>
        public static string ENTITYNAME = "Account";

        /// <summary>
        /// 列名Id,Id
        /// </summary>
        public static readonly string C_Id = "Id";

        /// <summary>
        /// 列名FirstName,FirstName
        /// </summary>
        public static readonly string C_FirstName = "FirstName";

        /// <summary>
        /// 列名LastName,LastName
        /// </summary>
        public static readonly string C_LastName = "LastName";

        /// <summary>
        /// 列名Email,Email
        /// </summary>
        public static readonly string C_Email = "Email";



        #endregion

        #region Field Members

        private Int32 _id;

        private String _firstName;

        private String _lastName;

        private String _email;



        #endregion

        #region Property Members

        /// <summary>
        /// 获取或设置Id
        /// </summary>
        [Column(Name = "Id", IsIdentity = true)]
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
        /// 获取或设置FirstName
        /// </summary>
        [Column(Name = "FirstName")]
        public virtual String FirstName
        {
            get
            {
                return this._firstName ?? String.Empty;
            }
            set
            {
                this._firstName = value;
            }
        }

        /// <summary>
        /// 获取或设置LastName
        /// </summary>
        [Column(Name = "LastName")]
        public virtual String LastName
        {
            get
            {
                return this._lastName ?? String.Empty;
            }
            set
            {
                this._lastName = value;
            }
        }

        /// <summary>
        /// 获取或设置Email
        /// </summary>
        [Column(Name = "Email")]
        public virtual String Email
        {
            get
            {
                return this._email ?? String.Empty;
            }
            set
            {
                this._email = value;
            }
        }

        #endregion
    }
}
