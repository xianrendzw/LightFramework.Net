using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data.Test.DTO
{
    public class NullableDTO
    {
        #region Const Members

        /// <summary>
        /// 实体Nullable名称
        /// </summary>
        public static string ENTITYNAME = "Nullable";

        /// <summary>
        /// 列名Id,Id
        /// </summary>
        public static readonly string C_Id = "Id";

        /// <summary>
        /// 列名TestBool,TestBool
        /// </summary>
        public static readonly string C_TestBool = "TestBool";

        /// <summary>
        /// 列名TestByte,TestByte
        /// </summary>
        public static readonly string C_TestByte = "TestByte";

        /// <summary>
        /// 列名TestChar,TestChar
        /// </summary>
        public static readonly string C_TestChar = "TestChar";

        /// <summary>
        /// 列名TestDateTime,TestDateTime
        /// </summary>
        public static readonly string C_TestDateTime = "TestDateTime";

        /// <summary>
        /// 列名TestDecimal,TestDecimal
        /// </summary>
        public static readonly string C_TestDecimal = "TestDecimal";

        /// <summary>
        /// 列名TestDouble,TestDouble
        /// </summary>
        public static readonly string C_TestDouble = "TestDouble";

        /// <summary>
        /// 列名TestGuid,TestGuid
        /// </summary>
        public static readonly string C_TestGuid = "TestGuid";

        /// <summary>
        /// 列名TestInt16,TestInt16
        /// </summary>
        public static readonly string C_TestInt16 = "TestInt16";

        /// <summary>
        /// 列名TestInt32,TestInt32
        /// </summary>
        public static readonly string C_TestInt32 = "TestInt32";

        /// <summary>
        /// 列名TestInt64,TestInt64
        /// </summary>
        public static readonly string C_TestInt64 = "TestInt64";

        /// <summary>
        /// 列名TestSingle,TestSingle
        /// </summary>
        public static readonly string C_TestSingle = "TestSingle";

        /// <summary>
        /// 列名TestTimeSpan,TestTimeSpan
        /// </summary>
        public static readonly string C_TestTimeSpan = "TestTimeSpan";



        #endregion

        #region Field Members

        private Int32 _id;

        private String _testBool;

        private Byte _testByte;

        private String _testChar;

        private DateTime _testDateTime;

        private Decimal _testDecimal;

        private String _testDouble;

        private Int64 _testGuid;

        private Int32 _testInt16;

        private Int32 _testInt32;

        private Int64 _testInt64;

        private Single _testSingle;

        private DateTime _testTimeSpan;



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
        /// 获取或设置TestBool
        /// </summary>
        [Column(Name = "TestBool")]
        public virtual String TestBool
        {
            get
            {
                return this._testBool ?? String.Empty;
            }
            set
            {
                this._testBool = value;
            }
        }

        /// <summary>
        /// 获取或设置TestByte
        /// </summary>
        [Column(Name = "TestByte")]
        public virtual Byte TestByte
        {
            get
            {
                return this._testByte;
            }
            set
            {
                this._testByte = value;
            }
        }

        /// <summary>
        /// 获取或设置TestChar
        /// </summary>
        [Column(Name = "TestChar")]
        public virtual String TestChar
        {
            get
            {
                return this._testChar ?? String.Empty;
            }
            set
            {
                this._testChar = value;
            }
        }

        /// <summary>
        /// 获取或设置TestDateTime
        /// </summary>
        [Column(Name = "TestDateTime")]
        public virtual DateTime TestDateTime
        {
            get
            {
                return this._testDateTime;
            }
            set
            {
                this._testDateTime = value;
            }
        }

        /// <summary>
        /// 获取或设置TestDecimal
        /// </summary>
        [Column(Name = "TestDecimal")]
        public virtual Decimal TestDecimal
        {
            get
            {
                return this._testDecimal;
            }
            set
            {
                this._testDecimal = value;
            }
        }

        /// <summary>
        /// 获取或设置TestDouble
        /// </summary>
        [Column(Name = "TestDouble")]
        public virtual String TestDouble
        {
            get
            {
                return this._testDouble ?? String.Empty;
            }
            set
            {
                this._testDouble = value;
            }
        }

        /// <summary>
        /// 获取或设置TestGuid
        /// </summary>
        [Column(Name = "TestGuid")]
        public virtual Int64 TestGuid
        {
            get
            {
                return this._testGuid;
            }
            set
            {
                this._testGuid = value;
            }
        }

        /// <summary>
        /// 获取或设置TestInt16
        /// </summary>
        [Column(Name = "TestInt16")]
        public virtual Int32 TestInt16
        {
            get
            {
                return this._testInt16;
            }
            set
            {
                this._testInt16 = value;
            }
        }

        /// <summary>
        /// 获取或设置TestInt32
        /// </summary>
        [Column(Name = "TestInt32")]
        public virtual Int32 TestInt32
        {
            get
            {
                return this._testInt32;
            }
            set
            {
                this._testInt32 = value;
            }
        }

        /// <summary>
        /// 获取或设置TestInt64
        /// </summary>
        [Column(Name = "TestInt64")]
        public virtual Int64 TestInt64
        {
            get
            {
                return this._testInt64;
            }
            set
            {
                this._testInt64 = value;
            }
        }

        /// <summary>
        /// 获取或设置TestSingle
        /// </summary>
        [Column(Name = "TestSingle")]
        public virtual Single TestSingle
        {
            get
            {
                return this._testSingle;
            }
            set
            {
                this._testSingle = value;
            }
        }

        /// <summary>
        /// 获取或设置TestTimeSpan
        /// </summary>
        [Column(Name = "TestTimeSpan")]
        public virtual DateTime TestTimeSpan
        {
            get
            {
                return this._testTimeSpan;
            }
            set
            {
                this._testTimeSpan = value;
            }
        }

        #endregion
    }
}
