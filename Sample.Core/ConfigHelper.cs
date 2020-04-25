using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;

namespace Sample.Core.Caching
{
    public class ConfigHelper
    {
        public static bool IsMemCacheEnabled
        {
            get
            {
                string isMemCacheEnabledStr = ConfigurationManager.AppSettings["IsMemCacheEnabled"];
                bool result = false;
                if (!String.IsNullOrWhiteSpace(isMemCacheEnabledStr))
                    bool.TryParse(isMemCacheEnabledStr, out result);
                return result;
            }
        }

        public static bool IsLearnPageCacheEnabled
        {
            get { return ConfigHelper.GetBoolValue("IsLearnPageCacheEnabled", false); }
        }
        public static bool IsMembasedOutputCacheEnabled
        {
            get { return ConfigHelper.GetBoolValue("IsMembasedOutputCacheEnabled", false); }
        }

        public static string MemcacheHost1 { get { return ConfigHelper.GetStringValue("MemcacheHost1"); } }
        public static int MemcachePort1 { get { return ConfigHelper.GetIntValue("MemcachePort1"); } }
        public static string MemcacheHost2 { get { return ConfigHelper.GetStringValue("MemcacheHost2"); } }
        public static int MemcachePort2 { get { return ConfigHelper.GetIntValue("MemcachePort2"); } }

        public static string MCacheOutputcacheHost1 { get { return ConfigHelper.GetStringValue("MCacheOutputcacheHost1"); } }
        public static int MCacheOutputcachePort1 { get { return ConfigHelper.GetIntValue("MCacheOutputcachePort1"); } }

        public static ServerInfo QueueServerInfo1 { get { return GetQueueServer("Queue1"); } }
        public static ServerInfo QueueServerInfo2 { get { return GetQueueServer("Queue2"); } }

        private static ServerInfo GetQueueServer(string queueName)
        {
            var queue = ConfigHelper.GetStringValue(queueName);
            String[] str = queue.ToString().Split(';');
            if (str.Length != 2) throw new Exception("Issue with QueueName: " + queueName);
            string host = str[0]; int port = Int32.Parse(str[1]);
            var serverInfo = new ServerInfo { Host = host, Port = port };
            return serverInfo;
        }

        public static bool GetBoolValue(string key, bool defaultValue)
        {
            var value = ConfigurationManager.AppSettings[key];
            if (value == null)
                return defaultValue;
            bool returnValue;
            if (!bool.TryParse(value, out returnValue))
                return defaultValue;
            return returnValue;
        }

        public static string GetStringValue(string key)
        {
            var value = ConfigurationManager.AppSettings[key];

            return value;
        }

        public static int GetIntValue(string key)
        {
            string value = ConfigurationManager.AppSettings[key];
            int result = 0;
            if (!Int32.TryParse(value, out result)) throw new ApplicationException("Config issue in the key" + key);

            return result;
        }

        public static string GetPerformance(double Percentile)
        {
            string performance = "";
            string Excellent = System.Configuration.ConfigurationManager.AppSettings["PerformanceCriteria"];
            String[] str = Excellent.ToString().Split(';');
            foreach (var strings in str)
            {
                string[] criteria = strings.Split(',');
                if (Percentile >= Convert.ToDouble(criteria[1]) && criteria[0].ToLower() != "very-poor")
                {
                    performance = criteria[0].Replace("-", " ");
                    return performance;
                }
                else if (Percentile <= Convert.ToDouble(criteria[1]) && criteria[0].ToLower() == "very-poor")
                {
                    performance = criteria[0].Replace("-", " "); ;
                    return performance;
                }
            }
            return performance;
        }

       

    }
    public class ServerInfo
    {
        public string Host { get; set; }
        public int Port { get; set; }
    }
}
