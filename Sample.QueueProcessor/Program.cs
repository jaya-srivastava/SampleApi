using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using iPractice.Core;
using iPractice.Models.DTO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Formatting;


namespace iPractice.QueueProcessor
{
    class Program
    {
        static bool IsDebug = true;
        static int i = 0;
        static Object obj = new object();
        static void Main(string[] args)
        {
            QueueReading();

        }

        private static void BulkRead()
        {
            for (int i = 0; i < 100000; i++)
            {
                var getResult = QueueMgr.GetString("WORK");
                if (getResult == null) break;
            }
        }

        public static void QueueReading()
        {
            Console.WriteLine(String.Format("start time: {0}", DateTime.Now.ToString()));
            string queueName = ConfigurationManager.AppSettings["QueueName"];

            //var sysInfo = SystemInfoMgr.GetSystemInfo();

            Console.WriteLine("Entering into Run mode");
            Console.WriteLine("Press ESC to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    var getResult = QueueMgr.Get<UserStatsDto>(queueName);

                    if (getResult == null) { System.Threading.Thread.Sleep(10000); }
                    else { SaveData2Db(getResult); }
                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            Console.WriteLine(String.Format("stop time: {0}", DateTime.Now.ToString()));
        }
        

        public static void QueueReadingDebug()
        {
            Console.WriteLine(String.Format("start time: {0}", DateTime.Now.ToString()));
            string queueName = ConfigurationManager.AppSettings["QueueName"];

            //var sysInfo = SystemInfoMgr.GetSystemInfo();

            Console.WriteLine("Entering into Run mode");
            Console.WriteLine("Press ESC to stop");
            do
            {
                while (!Console.KeyAvailable)
                {
                    //bool setResult = QueueMgr.Set<SystemDetail>(queueName, sysInfo);
                    var getResult = QueueMgr.Get<UserStatsDto>(queueName);
                    if (getResult == null) System.Threading.Thread.Sleep(10);
                    else if (IsDebug) { i++; Console.WriteLine(i.ToString()); }

                }
            } while (Console.ReadKey(true).Key != ConsoleKey.Escape);

            Console.WriteLine("It is complete");
            Console.WriteLine(String.Format("start time: {0}", DateTime.Now.ToString()));
        }

        private static void SaveData2Db(UserStatsDto postObject)
        {

            Uri requestUri = new Uri("");
            //call to Rest Service to save in db
            using (HttpClient client = new HttpClient())
            {
                HttpContent content = new ObjectContent<UserStatsDto>(postObject, GetMediaTypeFormatter());
                client.PostAsync(requestUri,content);
            }
        }

        private static MediaTypeFormatter GetMediaTypeFormatter()
        {
            //var jsonSerializerSettings = new JsonSerializerSettings();
            //jsonSerializerSettings.Converters.Add(new IsoDateTimeConverter());
            //var jsonFormatter = new JsonNetFormatter(jsonSerializerSettings);
            //return new MediaTypeFormatter[] { JsonMediaTypeFormatter };
            return new JsonMediaTypeFormatter();
        }
    }
}
