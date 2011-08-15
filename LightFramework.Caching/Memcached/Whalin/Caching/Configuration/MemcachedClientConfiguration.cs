using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using Whalin.Caching.Memcached;

namespace Whalin.Caching.Configuration
{
	/// <summary>
	/// Configuration class
	/// </summary>
	public class MemcachedClientConfiguration : IMemcachedClientConfiguration
	{
		/// <summary>
		/// Initializes a new instance of the <see cref="T:MemcachedClientConfiguration"/> class.
		/// </summary>
		public MemcachedClientConfiguration()
		{
			this.Servers = new List<IPEndPoint>();
			this.SocketPool = new SocketPoolConfiguration();
		}

		/// <summary>
		/// Adds a new server to the sockIOPool.
		/// </summary>
		/// <param name="address">The address and the port of the server in the format 'host:port'.</param>
		public void AddServer(string address)
		{
			this.Servers.Add(ConfigurationHelper.ResolveToEndPoint(address));
		}

		/// <summary>
		/// Adds a new server to the sockIOPool.
		/// </summary>
		/// <param name="address">The host name or IP address of the server.</param>
		/// <param name="port">The port number of the memcached instance.</param>
		public void AddServer(string host, int port)
		{
			this.Servers.Add(ConfigurationHelper.ResolveToEndPoint(host, port));
		}

		/// <summary>
		/// Gets a list of <see cref="T:IPEndPoint"/> each representing a Memcached server in the sockIOPool.
		/// </summary>
		public IList<IPEndPoint> Servers { get; private set; }

		/// <summary>
		/// Gets the configuration of the socket sockIOPool.
		/// </summary>
		public ISocketPoolConfiguration SocketPool { get; private set; }

		#region [ interface                     ]

		IList<System.Net.IPEndPoint> IMemcachedClientConfiguration.Servers
		{
			get { return this.Servers; }
		}

		ISocketPoolConfiguration IMemcachedClientConfiguration.SocketPool
		{
			get { return this.SocketPool; }
		}

        SockIOPool IMemcachedClientConfiguration.CreatePool()
        {
            try
            {
                string[] servers = this.Servers
                  .Select(ip => ip.Address.ToString() + ":" + ip.Port)
                  .ToArray();

                SockIOPool pool = SockIOPool.GetInstance(this.SocketPool.Name);
                pool.SetServers(servers);
                pool.InitConnections = this.SocketPool.IniSize;
                pool.MinConnections = this.SocketPool.MinSize;
                pool.MaxConnections = this.SocketPool.MaxSize;
                pool.SocketConnectTimeout = this.SocketPool.ConnectionTimeout.Milliseconds;
                pool.SocketTimeout = this.SocketPool.SocketTimeout.Milliseconds;
                pool.MaintenanceSleep = this.SocketPool.MaintenanceSleep.Milliseconds;
                pool.Failover = this.SocketPool.IsFailOver;
                pool.Nagle = this.SocketPool.IsNagle;
                pool.HashingAlgorithm = HashingAlgorithm.NewCompatibleHash;
                pool.Initialize();

                return pool;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

		#endregion


        
    }
}