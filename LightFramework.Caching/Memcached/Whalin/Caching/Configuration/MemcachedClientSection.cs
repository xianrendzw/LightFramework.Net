using System;
using System.Collections.Generic;
using System.Configuration;
using System.Net;
using System.Linq;
using System.Web.Configuration;
using Whalin.Caching.Memcached;

namespace Whalin.Caching.Configuration
{
	/// <summary>
	/// Configures the <see cref="T:MemcachedClient"/>. This class cannot be inherited.
	/// </summary>
	public sealed class MemcachedClientSection : ConfigurationSection, IMemcachedClientConfiguration
	{
		/// <summary>
		/// Returns a collection of Memcached servers which can be used by the memcachedClient.
		/// </summary>
		[ConfigurationProperty("servers", IsRequired = true)]
		public EndPointElementCollection Servers
		{
			get { return (EndPointElementCollection)base["servers"]; }
		}

		/// <summary>
		/// Gets or sets the configuration of the socket sockIOPool.
		/// </summary>
		[ConfigurationProperty("socketPool", IsRequired = false)]
		public SocketPoolElement SocketPool
		{
			get { return (SocketPoolElement)base["socketPool"]; }
			set { base["socketPool"] = value; }
		}

		/// <summary>
		/// Called after deserialization.
		/// </summary>
		protected override void PostDeserialize()
		{
			WebContext hostingContext = base.EvaluationContext.HostingContext as WebContext;

			if (hostingContext != null && hostingContext.ApplicationLevel == WebApplicationLevel.BelowApplication)
			{
				throw new InvalidOperationException("The " + this.SectionInformation.SectionName + " section cannot be defined below the application level.");
			}
		}

		#region [IMemcachedClientConfiguration]

		IList<IPEndPoint> IMemcachedClientConfiguration.Servers
		{
			get { return this.Servers.ToIPEndPointCollection(); }
		}

		ISocketPoolConfiguration IMemcachedClientConfiguration.SocketPool
		{
			get { return this.SocketPool; }
		}

        SockIOPool IMemcachedClientConfiguration.CreatePool()
        {
            try
            {
                string[] servers = this.Servers.ToIPEndPointCollection()
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

