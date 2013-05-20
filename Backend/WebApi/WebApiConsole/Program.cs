using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.SelfHost;
using System.Configuration;

namespace WebApiConsole
{
    class Program
    {
        private const string HostAddressConfigKey = "HostAddress";

        static void Main(string[] args)
        {
            try
            {
                var config = new HttpSelfHostConfiguration(ConfigurationManager.AppSettings[HostAddressConfigKey]);

                WebApi.WebApiConfig.Register(config);

                using (HttpSelfHostServer server = new HttpSelfHostServer(config))
                {
                    server.OpenAsync().Wait();
                    PrintAndWaitForInput(string.Format("Service hosted on address {0}", config.BaseAddress));
                }
            }
            catch (Exception e)
            {
                PrintAndWaitForInput(string.Format("{0}\n\r\n\r{1}", e.Message, e.StackTrace));
            }
        }

        private static void PrintAndWaitForInput(string message)
        {
            Console.WriteLine(message);
            Console.WriteLine("\n\rPress Enter to quit.");
            Console.ReadLine();
        }
    }
}
