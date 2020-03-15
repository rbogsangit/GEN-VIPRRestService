using Serilog.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Serilog;

namespace VIPRService.Helpers
{
    public class LogHelper
    {
        private static ILogger _log;
        public LogHelper()
        {
        }
        public static void Initiate(Logger log)
        {
            _log = log;
        }

        public static void Verbose(string message)
        {
            _log.Verbose(message);
        }
        public static void Debug(string message)
        {
            _log.Debug(message);
        }

        public static void Information(string message)
        {
            _log.Information(message);
        }
        public static void Warning(string message)
        {
            _log.Warning(message);
        }

        public static void Error(string message)
        {
            _log.Error(message);
        }
        public static void Error(Exception ex)
        {
            _log.Error(ex, ex.Message);            
        }

        public static void Fatal(string message)
        {
            _log.Fatal(message);
        }

    }
}
