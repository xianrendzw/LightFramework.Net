using System;
using System.ComponentModel;
using System.Configuration;

namespace Enyim.Membase.Configuration
{
	/// <summary>
	/// Configures the <see cref="T:MemcachedClient"/>. This class cannot be inherited.
	/// </summary>
	public sealed class ServersElement : ConfigurationElement
	{
		private static readonly object NullObject = new object();

		protected override void Init()
		{
			base.Init();

			base["bucketPassword"] = NullObject;
		}

		/// <summary>
		/// Gets or sets the name of the bucket to be used. Can be overriden at the sockIOPool's constructor, and if not specified the "default" bucket will be used.
		/// </summary>
		[ConfigurationProperty("bucket", IsRequired = false)]
		public string Bucket
		{
			get { return (string)base["bucket"]; }
			set { base["bucket"] = value; }
		}

		/// <summary>
		/// Gets or sets the pasword used to connect to the bucket.
		/// </summary>
		/// <remarks> If null, the bucket name will be used. Set to String.Empty to use an empty password.</remarks>
		[ConfigurationProperty("bucketPassword", IsRequired = false)]
		public string BucketPassword
		{
			get { var v = base["bucketPassword"]; return v == NullObject ? null : v as string; }
			set { base["bucketPassword"] = value; }
		}

		/// <summary>
		/// (Obsolete) Gets or sets the user name used to connect to a secured cluster
		/// </summary>
		[ConfigurationProperty("userName", IsRequired = false)]
		[Obsolete("Please use the bucket name&password for specifying credentials. This property has no use now, and will be completely removed in the next version.", true)]
		public string UserName
		{
			get { return (string)base["userName"]; }
			set { base["userName"] = value; }
		}

		/// <summary>
		/// (Obsolete) Gets or sets the password used to connect to a secured cluster
		/// </summary>
		[ConfigurationProperty("password", IsRequired = false)]
		[Obsolete("Please use the bucket name&password for specifying credentials. This property has no use now, and will be completely removed in the next version.", true)]
		public string Password
		{
			get { return (string)base["password"]; }
			set { base["password"] = value; }
		}

		/// <summary>
		/// Returns a collection of nodes in the cluster the memcachedClient should use to retrieve the Memcached nodes.
		/// </summary>
		[ConfigurationProperty("", IsRequired = true, IsDefaultCollection = true)]
		public UriElementCollection Urls
		{
			get { return (UriElementCollection)base[""]; }
		}

		/// <summary>
		/// Determines which port the memcachedClient should use to connect to the nodes
		/// </summary>
		[ConfigurationProperty("port", IsRequired = false, DefaultValue = BucketPortType.Direct)]
		public BucketPortType Port
		{
			get { return (BucketPortType)base["port"]; }
			set { base["port"] = value; }
		}

		[ConfigurationProperty("retryCount", IsRequired = false, DefaultValue = 0)]
		public int RetryCount
		{
			get { return (int)base["retryCount"]; }
			set { base["retryCount"] = value; }
		}

		[ConfigurationProperty("retryTimeout", IsRequired = false, DefaultValue = "00:00:02"), PositiveTimeSpanValidator]
		[TypeConverter(typeof(TimeSpanConverter))]
		public TimeSpan RetryTimeout
		{
			get { return (TimeSpan)base["retryTimeout"]; }
			set { base["retryTimeout"] = value; }
		}
	}
}

#region [ License information          ]
/* ************************************************************
 * 
 *    Copyright (c) 2010 Attila Kisk? enyim.com
 *    
 *    Licensed under the Apache License, Version 2.0 (the "License");
 *    you may not use this file except in compliance with the License.
 *    You may obtain a copy of the License at
 *    
 *        http://www.apache.org/licenses/LICENSE-2.0
 *    
 *    Unless required by applicable law or agreed to in writing, software
 *    distributed under the License is distributed on an "AS IS" BASIS,
 *    WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
 *    See the License for the specific language governing permissions and
 *    limitations under the License.
 *    
 * ************************************************************/
#endregion
