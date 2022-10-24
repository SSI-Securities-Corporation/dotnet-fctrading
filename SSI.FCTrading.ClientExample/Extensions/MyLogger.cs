using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SSI.FCTrading.ClientExample.Extensions
{
    public class MyLogger
    {
        public static ILogger CreateLogger()
        {
            string logTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss.fff zzz} [{Level:u3}] {Message:lj}{NewLine}{Exception}";
            var dateTime = DateTime.Now;
            string dateString = dateTime.ToString("dd-MM-yyyy");
            var logger = new LoggerConfiguration()
                        .MinimumLevel.Debug()
                        .WriteTo.Console(outputTemplate: logTemplate)
                        .WriteTo.File($"./log/log-{dateString}.txt", outputTemplate: logTemplate)
                        .CreateLogger();
            return logger;
        }
    }
}
