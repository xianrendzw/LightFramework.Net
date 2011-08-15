using System;
using System.Collections.Generic;
using System.Net;
using Whalin.Caching.Memcached;

namespace Whalin.Caching.Configuration
{
	/// <summary>
	/// Defines an interface for configuring the <see cref="T:MemcachedClient"/>.
	/// </summary>
	public interface IMemcachedClientConfiguration
	{
		/// <summary>
		/// Gets a list of <see cref="T:IPEndPoint"/> each representing a Memcached server in the sockIOPool.
		/// </summary>
		IList<IPEndPoint> Servers { get; }

		/// <summary>
		/// Gets the configuration of the socket sockIOPool.
		/// </summary>
		ISocketPoolConfiguration SocketPool { get; }

        /// <summary>
        /// Create the SockIOPool object.
        /// </summary>
        /// <returns></returns>
        SockIOPool CreatePool();
	}
}
