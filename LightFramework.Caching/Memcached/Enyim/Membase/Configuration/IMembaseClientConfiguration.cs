﻿using System;
using System.Collections.Generic;
using System.Net;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace Enyim.Membase.Configuration
{
	public interface IMembaseClientConfiguration
	{
		/// <summary>
		/// Gets the name of the bucket to be used. If not specified the "default" bucket will be used.
		/// </summary>
		string Bucket { get; }

		/// <summary>
		/// Gets the pasword used to connect to the bucket.
		/// </summary>
		/// <remarks> If null, the bucket name will be used. Set to String.Empty to use an empty password.</remarks>
		string BucketPassword { get; }

		/// <summary>
		/// Gets a list of <see cref="T:IPEndPoint"/> each representing a Memcached server in the sockIOPool.
		/// </summary>
		IList<Uri> Urls { get; }

		/// <summary>
		/// Gets the configuration of the socket sockIOPool.
		/// </summary>
		ISocketPoolConfiguration SocketPool { get; }

		/// <summary>
		/// Creates an <see cref="T:Enyim.Caching.Memcached.IMemcachedKeyTransformer"/> instance which will be used to convert item keys for Memcached.
		/// </summary>
		IMemcachedKeyTransformer CreateKeyTransformer();

		/// <summary>
		/// Creates an <see cref="T:Enyim.Caching.Memcached.IMemcachedNodeLocator"/> instance which will be used to assign items to Memcached nodes.
		/// </summary>
		IMemcachedNodeLocator CreateNodeLocator();

		/// <summary>
		/// Creates an <see cref="T:Enyim.Caching.Memcached.ITranscoder"/> instance which will be used to serialize or deserialize items.
		/// </summary>
		ITranscoder CreateTranscoder();

		/// <summary>
		/// Creates an <see cref="T:Enyim.Caching.Memcached.IPerformanceMonitor"/> instance which will be used to monitor the performance of the memcachedClient.
		/// </summary>
		IPerformanceMonitor CreatePerformanceMonitor();

		/// <summary>
		/// Gets the credentials used to connect to the REST API of the cluster.
		/// </summary>
		NetworkCredential Credentials { get; }

		/// <summary>
		/// Determines which port the memcachedClient should use to connect to the nodes
		/// </summary>
		BucketPortType Port { get; }

		TimeSpan RetryTimeout { get; }
		int RetryCount { get; }
	}
}

#region [ License information          ]
/* ************************************************************
 * 
 *    Copyright (c) 2010 Attila Kiskó, enyim.com
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
