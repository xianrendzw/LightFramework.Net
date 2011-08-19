using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data.Test.DTO
{
    public class ItemDTO
    {
        #region Const Members

        /// <summary>
        /// 实体Item名称
        /// </summary>
        public static string ENTITYNAME = "Item";

        /// <summary>
        /// 列名Id,Id
        /// </summary>
        public static readonly string C_Id = "Id";

        /// <summary>
        /// 列名ProductId,ProductId
        /// </summary>
        public static readonly string C_ProductId = "ProductId";

        /// <summary>
        /// 列名listPrice,listPrice
        /// </summary>
        public static readonly string C_listPrice = "listPrice";

        /// <summary>
        /// 列名UnitCost,UnitCost
        /// </summary>
        public static readonly string C_UnitCost = "UnitCost";

        /// <summary>
        /// 列名Currency,Currency
        /// </summary>
        public static readonly string C_Currency = "Currency";

        /// <summary>
        /// 列名Photo,Photo
        /// </summary>
        public static readonly string C_Photo = "Photo";

        /// <summary>
        /// 列名Quantity,Quantity
        /// </summary>
        public static readonly string C_Quantity = "Quantity";

        /// <summary>
        /// 列名Attribute1,Attribute1
        /// </summary>
        public static readonly string C_Attribute1 = "Attribute1";

        /// <summary>
        /// 列名Status,Status
        /// </summary>
        public static readonly string C_Status = "Status";



        #endregion

        #region Field Members

        private Int32 _id;

        private Int32 _productId;

        private Decimal _listPrice;

        private Decimal _unitCost;

        private String _currency;

        private String _photo;

        private Int32 _quantity;

        private String _attribute1;

        private String _status;



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
        /// 获取或设置ProductId
        /// </summary>
        [Column(Name = "ProductId")]
        public virtual Int32 ProductId
        {
            get
            {
                return this._productId;
            }
            set
            {
                this._productId = value;
            }
        }

        /// <summary>
        /// 获取或设置listPrice
        /// </summary>
        [Column(Name = "listPrice")]
        public virtual Decimal listPrice
        {
            get
            {
                return this._listPrice;
            }
            set
            {
                this._listPrice = value;
            }
        }

        /// <summary>
        /// 获取或设置UnitCost
        /// </summary>
        [Column(Name = "UnitCost")]
        public virtual Decimal UnitCost
        {
            get
            {
                return this._unitCost;
            }
            set
            {
                this._unitCost = value;
            }
        }

        /// <summary>
        /// 获取或设置Currency
        /// </summary>
        [Column(Name = "Currency")]
        public virtual String Currency
        {
            get
            {
                return this._currency ?? String.Empty;
            }
            set
            {
                this._currency = value;
            }
        }

        /// <summary>
        /// 获取或设置Photo
        /// </summary>
        [Column(Name = "Photo")]
        public virtual String Photo
        {
            get
            {
                return this._photo ?? String.Empty;
            }
            set
            {
                this._photo = value;
            }
        }

        /// <summary>
        /// 获取或设置Quantity
        /// </summary>
        [Column(Name = "Quantity")]
        public virtual Int32 Quantity
        {
            get
            {
                return this._quantity;
            }
            set
            {
                this._quantity = value;
            }
        }

        /// <summary>
        /// 获取或设置Attribute1
        /// </summary>
        [Column(Name = "Attribute1")]
        public virtual String Attribute1
        {
            get
            {
                return this._attribute1 ?? String.Empty;
            }
            set
            {
                this._attribute1 = value;
            }
        }

        /// <summary>
        /// 获取或设置Status
        /// </summary>
        [Column(Name = "Status")]
        public virtual String Status
        {
            get
            {
                return this._status ?? String.Empty;
            }
            set
            {
                this._status = value;
            }
        }

        #endregion
    }
}
