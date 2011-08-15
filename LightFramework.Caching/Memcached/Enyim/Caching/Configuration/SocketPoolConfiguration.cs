﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Enyim.Caching.Configuration
{
	public class SocketPoolConfiguration : ISocketPoolConfiguration
	{
		private int minPoolSize = 10;
		private int maxPoolSize = 20;
		private TimeSpan connectionTimeout = new TimeSpan(0, 0, 10);
		private TimeSpan receiveTimeout = new TimeSpan(0, 0, 10);
		private TimeSpan deadTimeout = new TimeSpan(0, 0, 10);
		private TimeSpan queueTimeout = new TimeSpan(0, 0, 0, 0, 100);

		int ISocketPoolConfiguration.MinPoolSize
		{
			get { return this.minPoolSize; }
			set
			{
				if (value > this.maxPoolSize)
					throw new ArgumentOutOfRangeException("value", "MinPoolSize must be <= MaxPoolSize!");

				this.minPoolSize = value;
			}
		}

		/// <summary>
		/// Gets or sets a value indicating the maximum amount of sockets per server in the socket sockIOPool.
		/// </summary>
		/// <returns>The maximum amount of sockets per server in the socket sockIOPool. The default is 20.</returns>
		/// <remarks>It should be 0.75 * (number of threads) for optimal performance.</remarks>
		int ISocketPoolConfiguration.MaxPoolSize
		{
			get { return this.maxPoolSize; }
			set
			{
				if (value < this.minPoolSize)
					throw new ArgumentOutOfRangeException("value", "MaxPoolSize must be >= MinPoolSize!");

				this.maxPoolSize = value;
			}
		}

		TimeSpan ISocketPoolConfiguration.ConnectionTimeout
		{
			get { return this.connectionTimeout; }
			set
			{
				if (value < TimeSpan.Zero)
					throw new ArgumentOutOfRangeException("value", "value must be positive");

				this.connectionTimeout = value;
			}
		}

		TimeSpan ISocketPoolConfiguration.ReceiveTimeout
		{
			get { return this.receiveTimeout; }
			set
			{
				if (value < TimeSpan.Zero)
					throw new ArgumentOutOfRangeException("value", "value must be positive");

				this.receiveTimeout = value;
			}
		}

		TimeSpan ISocketPoolConfiguration.QueueTimeout
		{
			get { return this.queueTimeout; }
			set
			{
				if (value < TimeSpan.Zero)
					throw new ArgumentOutOfRangeException("value", "value must be positive");

				this.queueTimeout = value;
			}
		}

		TimeSpan ISocketPoolConfiguration.DeadTimeout
		{
			get { return this.deadTimeout; }
			set
			{
				if (value < TimeSpan.Zero)
					throw new ArgumentOutOfRangeException("value", "value must be positive");

				this.deadTimeout = value;
			}
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
