using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Whalin.Caching.Configuration
{
	public class SocketPoolConfiguration : ISocketPoolConfiguration
	{
        private string name = "memcached";
        private int iniSize = 10;
		private int minPoolSize = 10;
		private int maxPoolSize = 20;
        private bool isFailOver = true;
        private bool isNagle = true;
        private TimeSpan connectionTimeout = new TimeSpan(0, 0, 0, 1000);
		private TimeSpan receiveTimeout = new TimeSpan(0, 0, 10);
		private TimeSpan deadTimeout = new TimeSpan(0, 0, 10);
        private TimeSpan socketTimeout = new TimeSpan(0, 0, 0, 0, 3000);
		private TimeSpan queueTimeout = new TimeSpan(0, 0, 0, 0, 100);
        private TimeSpan maintenanceSleep = new TimeSpan(0, 0, 0, 0, 30);

        String ISocketPoolConfiguration.Name
        {
            get { return this.name; }
            set { this.name = value; }
        }

        int ISocketPoolConfiguration.IniSize
        {
            get { return this.iniSize; }
            set
            {
                if (value > this.maxPoolSize || value < this.minPoolSize)
                    throw new ArgumentOutOfRangeException("value", "PoolSize must be <= MaxPoolSize or PoolSize must be >= MinPoolSize!");

                this.iniSize = value;
            }
        }

		int ISocketPoolConfiguration.MinSize
		{
			get { return this.minPoolSize; }
			set
			{
				if (value > this.maxPoolSize)
					throw new ArgumentOutOfRangeException("value", "MinPoolSize must be <= MaxPoolSize!");

				this.minPoolSize = value;
			}
		}

		int ISocketPoolConfiguration.MaxSize
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

        TimeSpan ISocketPoolConfiguration.SocketTimeout
        {
            get { return this.socketTimeout; }
            set
            {
                if (value < TimeSpan.Zero)
                    throw new ArgumentOutOfRangeException("value", "value must be positive");

                this.socketTimeout = value;
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

        TimeSpan ISocketPoolConfiguration.MaintenanceSleep
        {
            get { return this.maintenanceSleep; }
            set
            {
                if (value < TimeSpan.Zero)
                    throw new ArgumentOutOfRangeException("value", "value must be positive");

                this.maintenanceSleep = value;
            }
        }

        Boolean ISocketPoolConfiguration.IsFailOver
        {
            get { return this.isFailOver; }
            set { this.isFailOver = value; }
        }

        Boolean ISocketPoolConfiguration.IsNagle
        {
            get { return this.isNagle; }
            set { this.isNagle = value; }
        }
    }
}