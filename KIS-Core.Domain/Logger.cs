using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Serilog;

namespace KIS_Core.Domain
{
    public static class Logger
    {
        private static readonly ILogger _errorLogger;

        static Logger()
        {
            _errorLogger = new LoggerConfiguration()
                .WriteTo.File("C:/home/site/wwwroot/wwwroot/assets/lo/KIS-Core-Logger.txt", rollingInterval: RollingInterval.Day)
                .CreateLogger();
        }

        public static void LogError(string error)
        {
            _errorLogger.Error(error);
        }
    }
}
