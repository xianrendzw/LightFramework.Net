using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LightFramework.Data
{
    /// <summary>
    /// Maps a standard column of the table.
    /// </summary>
    /// <example>
    /// In the following example, the column is also
    /// called 'name', so you don't have to specify.
    /// <code>
    /// public class Blog 
    /// {
    ///		...
    ///		
    ///		[Column]
    ///		public int Name
    ///		{
    ///			get { return _name; }
    ///			set { _name = value; }
    ///		}
    ///	</code>
    /// To map a column name, use 
    /// <code>
    ///		[Column("blog_name")]
    ///		public int Name
    ///		{
    ///			get { return _name; }
    ///			set { _name = value; }
    ///		}
    ///	</code>
    /// </example>
    [AttributeUsage(AttributeTargets.Property), Serializable]
    public class ColumnAttribute : Attribute
    {
        /// <summary>
        /// Initializes a new instance of theclass.
        /// </summary>
        public ColumnAttribute()
        {
        }

        /// <summary>
        /// Initializes a new instance of the class.
        /// </summary>
        /// <param name="column">The column.</param>
        public ColumnAttribute(String name)
            : this()
        {
            Name = name;
        }

        /// <summary>
        /// Initializes a new instance of the  class.
        /// </summary>
        /// <param name="column">The column.</param>
        /// <param name="type">The type.</param>
        public ColumnAttribute(String name, String type)
            : this(name)
        {
            ColumnType = type;
        }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ColumnAttribute"/> is Identity.
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this property allow null.
        /// </summary>
        /// <value><c>true</c> if allow null; otherwise, <c>false</c>.</value>
        public bool IsNullable { get; set; }

        /// <summary>
        /// Gets or sets the length of the property (for strings - nvarchar(50) )
        /// </summary>
        /// <value>The length.</value>
        public int Length { get; set; }

        /// <summary>
        /// Gets or sets the column name
        /// </summary>
        /// <value>The column.</value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ColumnAttribute"/> is IsPrimaryKey.
        /// </summary>
        /// <value><c>true</c> if IsPrimaryKey; otherwise, <c>false</c>.</value>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ColumnAttribute"/> is IsForeignKey.
        /// </summary>
        /// <value><c>true</c> if IsForeignKey; otherwise, <c>false</c>.</value>
        public bool IsForeignKey { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="ColumnAttribute"/> is unique.
        /// </summary>
        /// <value><c>true</c> if unique; otherwise, <c>false</c>.</value>
        public bool Unique { get; set; }

        /// <summary>
        /// Gets or sets the formula used to calculate this property
        /// </summary>
        /// <value>The formula.</value>
        public string Formula { get; set; }

        /// <summary>
        /// Gets or sets the type of the column.
        /// </summary>
        /// <value>The type of the column.</value>
        public string ColumnType { get; set; }

        /// <summary>
        /// A unique-key attribute can be used to group columns 
        /// in a single unit key constraint. 
        /// </summary>
        /// <value>unique key name</value>
        /// <remarks>
        /// Currently, the 
        /// specified value of the unique-key attribute is not 
        /// used to name the constraint, only to group the columns 
        /// in the mapping file.
        /// </remarks>
        public string UniqueKey { get; set; }

        /// <summary>
        /// specifies the name of a (multi-column) index
        /// </summary>
        /// <value>index name</value>
        public string Index { get; set; }

        /// <summary>
        /// overrides the default column type
        /// </summary>
        /// <value>column_type</value>
        public string SqlType { get; set; }

        /// <summary>
        /// create an SQL check constraint on either column or table
        /// </summary>
        /// <value>Sql Expression</value>
        public string Check { get; set; }

        /// <summary>
        /// Gets or sets the default value for a column (used by schema generation). 
        /// Please note that you should be careful to set Insert=false or set the value to the same 
        /// as the default on the database. 
        /// </summary>
        /// <value>The default value for the column.</value>
        public string Default { get; set; }

        /// <summary>
        /// Set to <c>true</c> if this property overrides a property in a base class
        /// </summary>
        public bool IsOverride { get; set; }
    }
}
