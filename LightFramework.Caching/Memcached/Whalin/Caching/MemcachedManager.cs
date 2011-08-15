using System;
using System.Collections;
using System.Configuration;
using System.Linq;

namespace Whalin.Caching
{
    using Memcached;
    using Configuration;

    /// <summary>
    /// Memcached管理操作类
    /// </summary>
    public sealed class MemcachedManager
    {
        private static MemcachedClient memcachedClient;
        private static SockIOPool sockIOPool;
        private static string[] servers;

        /// <summary>
        /// Initializes a new MemcachedClient instance using the default configuration section (caching/whalinMemcached).
        /// </summary>
        public static MemcachedClient CreateClient()
        {
            MemcachedClientSection DefaultSettings = ConfigurationManager.GetSection("caching/whalinMemcached") as MemcachedClientSection;
            return CreateClient(DefaultSettings);
        }

        /// <summary>
        /// Initializes a new MemcachedClient instance using the specified configuration section. 
        /// This overload allows to create multiple MemcachedClients with different sockIOPool configurations.
        /// </summary>
        /// <param name="sectionName">The name of the configuration section to be used for configuring the behavior of the memcachedClient.</param>
        public static MemcachedClient CreateClient(string sectionName)
        {
            return CreateClient(GetSection(sectionName));
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="T:MemcachedClient"/> using the specified configuration instance.
        /// </summary>
        /// <param name="configuration">The memcachedClient configuration.</param>
        public static MemcachedClient CreateClient(IMemcachedClientConfiguration configuration)
        {
            if (configuration == null)
                throw new ArgumentNullException("configuration");

            sockIOPool = configuration.CreatePool();
            servers = configuration.Servers.Select(ip => ip.Address.ToString() + ":" + ip.Port).ToArray();

            memcachedClient = new MemcachedClient();
            memcachedClient.PoolName = sockIOPool.Name;
            memcachedClient.EnableCompression = false;

            return memcachedClient;
        }

        private static IMemcachedClientConfiguration GetSection(string sectionName)
        {
            MemcachedClientSection section = (MemcachedClientSection)ConfigurationManager.GetSection(sectionName);
            if (section == null)
                throw new ConfigurationErrorsException("Section " + sectionName + " is not found.");

            return section;
        }

        /// <summary>
        /// 缓存服务器地址列表(IP:Port)
        /// </summary>
        public static string[] Servers
        {
            set
            {
                if (value != null)
                    servers = value;
            }
            get { return servers; }
        }

        public static void Dispose()
        {
            if (sockIOPool != null)
                sockIOPool.Shutdown();
        }

        /// <summary>
        /// 获取当前缓存键值所存储在的服务器
        /// </summary>
        /// <param name="key">当前缓存键</param>
        /// <returns>当前缓存键值所存储在的服务器</returns>
        public static string GetSocketHost(string key)
        {
            string hostName = "";
            SockIO sock = null;

            try
            {
                sock = SockIOPool.GetInstance(sockIOPool.Name).GetSock(key);
                if (sock != null)
                    hostName = sock.Host;
            }
            finally
            {
                if (sock != null)
                    sock.Close();
            }

            return hostName;
        }

        /// <summary>
        /// 获取有效的服务器地址
        /// </summary>
        /// <returns>有效的服务器地</returns>
        public static string[] GetConnectedSocketHost()
        {
            SockIO sock = null;
            string connectedHost = null;

            foreach (string server in servers)
            {
                if (string.IsNullOrEmpty(server))
                    continue;

                try
                {
                    sock = SockIOPool.GetInstance(sockIOPool.Name).GetConnection(server);
                    if (sock != null)
                        connectedHost = connectedHost + server;
                }
                finally
                {
                    if (sock != null)
                        sock.Close();
                }
            }

            return connectedHost.Split(',');
        }

        /// <summary>
        /// 获取服务器端缓存的数据信息
        /// </summary>
        /// <returns>返回信息</returns>
        public static ArrayList GetStats()
        {
            ArrayList arrayList = new ArrayList();
            foreach (string server in servers)
            {
                arrayList.Add(server);
            }

            return GetStats(arrayList, Stats.Default, null);
        }

        /// <summary>
        /// 获取服务器端缓存的数据信息
        /// </summary>
        /// <param name="serverArrayList">要访问的服务列表</param>
        /// <returns>返回信息</returns>
        public static ArrayList GetStats(ArrayList serverArrayList, Stats statsCommand, string param)
        {
            ArrayList statsArray = new ArrayList();
            param = string.IsNullOrEmpty(param) ? "" : param.Trim().ToLower();

            string commandstr = "stats";
            //转换stats命令参数
            switch (statsCommand)
            {
                case Stats.Reset: { commandstr = "stats reset"; break; }
                case Stats.Malloc: { commandstr = "stats malloc"; break; }
                case Stats.Maps: { commandstr = "stats maps"; break; }
                case Stats.Sizes: { commandstr = "stats sizes"; break; }
                case Stats.Slabs: { commandstr = "stats slabs"; break; }
                case Stats.Items: { commandstr = "stats"; break; }
                case Stats.CachedDump:
                    {
                        string[] statsparams = param.Split(new string[] { " " }, StringSplitOptions.None);
                        if (statsparams.Length == 2)
                            //if (ValidateHelper.IsNumericArray(statsparams))
                            commandstr = "stats cachedump  " + param;

                        break;
                    }
                case Stats.Detail:
                    {
                        if (string.Equals(param, "on") || string.Equals(param, "off") || string.Equals(param, "dump"))
                            commandstr = "stats detail " + param.Trim();

                        break;
                    }
                default: { commandstr = "stats"; break; }
            }
            //加载返回值
            Hashtable stats = memcachedClient.Stats(serverArrayList, commandstr);
            foreach (string key in stats.Keys)
            {
                statsArray.Add(key);
                Hashtable values = (Hashtable)stats[key];
                foreach (string key2 in values.Keys)
                {
                    statsArray.Add(key2 + ":" + values[key2]);
                }
            }
            return statsArray;
        }

        /// <summary>
        /// Stats命令行参数
        /// </summary>
        public enum Stats
        {
            /// <summary>
            /// stats : 显示服务器信息, 统计数据等
            /// </summary>
            Default = 0,
            /// <summary>
            /// stats reset : 清空统计数据
            /// </summary>
            Reset = 1,
            /// <summary>
            /// stats malloc : 显示内存分配数据
            /// </summary>
            Malloc = 2,
            /// <summary>
            /// stats maps : 显示"/proc/self/maps"数据
            /// </summary>
            Maps = 3,
            /// <summary>
            /// stats sizes
            /// </summary>
            Sizes = 4,
            /// <summary>
            /// stats slabs : 显示各个slab的信息,包括chunk的大小,数目,使用情况等
            /// </summary>
            Slabs = 5,
            /// <summary>
            /// stats items : 显示各个slab中item的数目和最老item的年龄(最后一次访问距离现在的秒数)
            /// </summary>
            Items = 6,
            /// <summary>
            /// stats cachedump slab_id limit_num : 显示某个slab中的前 limit_num 个 key 列表
            /// </summary>
            CachedDump = 7,
            /// <summary>
            /// stats detail [on|off|dump] : 设置或者显示详细操作记录   on:打开详细操作记录  off:关闭详细操作记录 dump: 显示详细操作记录(每一个键值get,set,hit,del的次数)
            /// </summary>
            Detail = 8
        }
    }
}
