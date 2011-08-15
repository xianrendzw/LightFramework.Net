using System;
using System.ComponentModel;
using System.Configuration;

namespace Whalin.Caching.Configuration
{
	/// <summary>
	/// Configures the socket sockIOPool settings for Memcached servers.
	/// </summary>
	public sealed class SocketPoolElement : ConfigurationElement, ISocketPoolConfiguration
	{
        /// <summary>
        /// Sets the sockIOPool that this instance of the memcachedClient will use.
        /// The sockIOPool must already be initialized or none of this will work.
        /// </summary>
        [ConfigurationProperty("name", IsRequired = false, DefaultValue = "memcached")]
        public String Name
        {
            get { return base["name"].ToString(); }
            set { base["name"] = value; }
        }

        /// <summary>
        /// Gets or sets a value indicating the initial amount of sockets per server in the socket sockIOPool.
        /// </summary>
        /// <returns>The initial amount of sockets per server in the socket sockIOPool.</returns>
        [ConfigurationProperty("iniSize", IsRequired = false, DefaultValue = 10), IntegerValidator(MinValue = 0)]
        public int IniSize
        {
            get { return (int)base["iniSize"]; }
            set { base["iniSize"] = value; }
        }

		/// <summary>
		/// Gets or sets a value indicating the minimum amount of sockets per server in the socket sockIOPool.
		/// </summary>
		/// <returns>The minimum amount of sockets per server in the socket sockIOPool.</returns>
        [ConfigurationProperty("minSize", IsRequired = false, DefaultValue = 10), IntegerValidator(MinValue = 0)]
		public int MinSize
		{
			get { return (int)base["minSize"]; }
			set { base["minSize"] = value; }
		}

		/// <summary>
		/// Gets or sets a value indicating the maximum amount of sockets per server in the socket sockIOPool.
		/// </summary>
		/// <returns>The maximum amount of sockets per server in the socket sockIOPool. The default is 20.</returns>
		/// <remarks>It should be 0.75 * (number of threads) for optimal performance.</remarks>
		[ConfigurationProperty("maxSize", IsRequired = false, DefaultValue = 20), IntegerValidator(MinValue = 0)]
		public int MaxSize
		{
            get { return (int)base["maxSize"]; }
            set { base["maxSize"] = value; }
		}

		/// <summary>
		/// Gets or sets a value that specifies the amount of time after which the connection attempt will fail.
		/// </summary>
        /// <returns>The value of the connection timeout. The default is 100 milliseconds.</returns>
        [ConfigurationProperty("connectionTimeout", IsRequired = false, DefaultValue = "00:00:00.100"), PositiveTimeSpanValidator, TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan ConnectionTimeout
		{
			get { return (TimeSpan)base["connectionTimeout"]; }
			set { base["connectionTimeout"] = value; }
		}


        /// Gets or sets the socket timeout for reads
        /// </summary>
        /// <value>timeout time in milliseconds.The default is 100 milliseconds.</value>
        [ConfigurationProperty("socketTimeout", IsRequired = false, DefaultValue = "00:00:00.100"), PositiveTimeSpanValidator, TypeConverter(typeof(InfiniteTimeSpanConverter))]
        public TimeSpan SocketTimeout
        {
            get { return (TimeSpan)base["socketTimeout"]; }
            set { base["socketTimeout"] = value; }
        }

		/// <summary>
		/// Gets or sets a value that specifies the amount of time after which the getting a connection from the sockIOPool will fail. The default is 100 msec.
		/// </summary>
		/// <returns>The value of the queue timeout.</returns>
		[ConfigurationProperty("queueTimeout", IsRequired = false, DefaultValue = "00:00:00.100"), PositiveTimeSpanValidator, TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan QueueTimeout
		{
			get { return (TimeSpan)base["queueTimeout"]; }
			set { base["queueTimeout"] = value; }
		}

		/// <summary>
		/// Gets or sets a value that specifies the amount of time after which receiving data from the socket fails.
		/// </summary>
		/// <returns>The value of the receive timeout. The default is 10 seconds.</returns>
		[ConfigurationProperty("receiveTimeout", IsRequired = false, DefaultValue = "00:00:10"), PositiveTimeSpanValidator, TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan ReceiveTimeout
		{
			get { return (TimeSpan)base["receiveTimeout"]; }
			set { base["receiveTimeout"] = value; }
		}

		/// <summary>
		/// Gets or sets a value that specifies the amount of time after which an unresponsive (dead) server will be checked if it is working.
		/// </summary>
		/// <returns>The value of the dead timeout. The default is 10 secs.</returns>
		[ConfigurationProperty("deadTimeout", IsRequired = false, DefaultValue = "00:00:10"), PositiveTimeSpanValidator, TypeConverter(typeof(InfiniteTimeSpanConverter))]
		public TimeSpan DeadTimeout
		{
			get { return (TimeSpan)base["deadTimeout"]; }
			set { base["deadTimeout"] = value; }
		}

        /// <summary>
        /// Gets or sets the sleep time between runs of the sockIOPool maintenance thread.
        /// If set to 0, then the maintenance thread will not be started;
        /// </summary>
        /// <value>sleep time in milliseconds</value>
        [ConfigurationProperty("maintenanceSleep", IsRequired = false, DefaultValue = "00:00:00.30"), PositiveTimeSpanValidator, TypeConverter(typeof(InfiniteTimeSpanConverter))]
        public TimeSpan MaintenanceSleep
        {
            get { return (TimeSpan)base["maintenanceSleep"]; }
            set { base["maintenanceSleep"] = value; }
        }

        /// <summary>
        /// Gets or sets the failover flag for the sockIOPool.
        /// If this flag is set to true and a socket fails to connect,
        /// the sockIOPool will attempt to return a socket from another server
        /// if one exists.  If set to false, then getting a socket
        /// will return null if it fails to connect to the requested server.
        /// </summary>
        [ConfigurationProperty("isFailOver", IsRequired = false, DefaultValue = true)]
        public bool IsFailOver
        {
            get { return (bool)base["isFailOver"]; }
            set { base["isFailOver"] = value; }
        }

        /// <summary>
        /// Gets or sets the Nagle algorithm flag for the sockIOPool.
        /// If false, will turn off Nagle's algorithm on all sockets created.
        /// </summary>
        [ConfigurationProperty("isNagle", IsRequired = false, DefaultValue = true)]
        public bool IsNagle
        {
            get { return (bool)base["isNagle"]; }
            set { base["isNagle"] = value; }
        }

		/// <summary>
		/// Called after deserialization.
		/// </summary>
		protected override void PostDeserialize()
		{
			base.PostDeserialize();

			if (this.MinSize > this.MaxSize)
				throw new ConfigurationErrorsException("maxPoolSize must be larger than minPoolSize.");
		}

		#region [ ISocketPoolConfiguration     ]

        String ISocketPoolConfiguration.Name
        {
            get { return this.Name; }
            set { this.Name = value; }
        }

        int ISocketPoolConfiguration.IniSize
        {
            get { return this.IniSize; }
            set { this.IniSize = value; }
        }

        int ISocketPoolConfiguration.MinSize
        {
            get { return this.IniSize; }
            set { this.IniSize = value; }
        }

        int ISocketPoolConfiguration.MaxSize
        {
            get { return this.IniSize; }
            set { this.IniSize = value; }
        }

        TimeSpan ISocketPoolConfiguration.ConnectionTimeout
        {
            get { return this.ConnectionTimeout; }
            set { this.ConnectionTimeout = value; }
        }

        TimeSpan ISocketPoolConfiguration.SocketTimeout
        {
            get { return this.SocketTimeout; }
            set { this.SocketTimeout = value; }
        }

        TimeSpan ISocketPoolConfiguration.ReceiveTimeout
        {
            get { return this.ReceiveTimeout; }
            set { this.ReceiveTimeout = value; }
        }

        TimeSpan ISocketPoolConfiguration.QueueTimeout
        {
            get { return this.QueueTimeout; }
            set { this.QueueTimeout = value; }
        }

        TimeSpan ISocketPoolConfiguration.DeadTimeout
        {
            get { return this.DeadTimeout; }
            set { this.DeadTimeout = value; }
        }

        TimeSpan ISocketPoolConfiguration.MaintenanceSleep
        {
            get { return this.MaintenanceSleep; }
            set { this.MaintenanceSleep = value; }
        }

        Boolean ISocketPoolConfiguration.IsFailOver
        {
            get { return this.IsFailOver; }
            set { this.IsFailOver = value; }
        }

        Boolean ISocketPoolConfiguration.IsNagle
        {
            get { return this.IsNagle; }
            set { this.IsNagle = value; }
        }

		#endregion
    }
}
