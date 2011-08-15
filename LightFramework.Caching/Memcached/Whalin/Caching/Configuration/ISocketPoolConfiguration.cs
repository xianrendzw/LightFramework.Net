using System;

namespace Whalin.Caching.Configuration
{
	/// <summary>
	/// Defines an interface for configuring the socket sockIOPool for the <see cref="T:MemcachedClient"/>.
	/// </summary>
	public interface ISocketPoolConfiguration
	{
        /// <summary>
        /// Gets or sets the name of the socket sockIOPool.
        /// </summary>
        String Name
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets a value indicating the initial amount of sockets per server in the socket sockIOPool.
        /// </summary>
        /// <returns>The initial amount of sockets per server in the socket sockIOPool.</returns>
        int IniSize
        {
            get;
            set;
        }

		/// <summary>
		/// Gets or sets a value indicating the minimum amount of sockets per server in the socket sockIOPool.
		/// </summary>
		/// <returns>The minimum amount of sockets per server in the socket sockIOPool.</returns>
		int MinSize
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value indicating the maximum amount of sockets per server in the socket sockIOPool.
		/// </summary>
		/// <returns>The maximum amount of sockets per server in the socket sockIOPool.</returns>
		int MaxSize
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value that specifies the amount of time after which the connection attempt will fail.
		/// </summary>
        /// <returns>The value of the connection timeout(milliseconds).</returns>
		TimeSpan ConnectionTimeout
		{
			get;
			set;
		}

        /// <summary>
        /// Gets or sets the socket timeout for reads
        /// </summary>
        /// <value>timeout time in milliseconds</value>
        TimeSpan SocketTimeout
        {
            get;
            set;
        }

		/// <summary>
		/// Gets or sets a value that specifies the amount of time after which the getting a connection from the sockIOPool will fail.
		/// </summary>
		/// <returns>The value of the queue timeout.</returns>
		TimeSpan QueueTimeout
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value that specifies the amount of time after which receiving data from the socket will fail.
		/// </summary>
		/// <returns>The value of the receive timeout.</returns>
		TimeSpan ReceiveTimeout
		{
			get;
			set;
		}

		/// <summary>
		/// Gets or sets a value that specifies the amount of time after which an unresponsive (dead) server will be checked if it is working.
		/// </summary>
		/// <returns>The value of the dead timeout.</returns>
		TimeSpan DeadTimeout
		{
			get;
			set;
		}

        /// <summary>
        /// Gets or sets the sleep time between runs of the sockIOPool maintenance thread.
        /// If set to 0, then the maintenance thread will not be started;
        /// </summary>
        /// <value>sleep time in milliseconds</value>
        TimeSpan MaintenanceSleep
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the failover flag for the sockIOPool.
        /// 
        /// If this flag is set to true and a socket fails to connect,
        /// the sockIOPool will attempt to return a socket from another server
        /// if one exists.  If set to false, then getting a socket
        /// will return null if it fails to connect to the requested server.
        /// </summary>
        Boolean IsFailOver
        {
            get;
            set;
        }

        /// <summary>
        /// Gets or sets the Nagle algorithm flag for the sockIOPool.
        /// If false, will turn off Nagle's algorithm on all sockets created.
        /// </summary>
        Boolean IsNagle
        {
            get;
            set;
        }
	}
}
