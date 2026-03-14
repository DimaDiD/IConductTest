using Serilog;
using System;
using System.IO;
using System.Web.Hosting;

namespace EmployeeService
{
    public static class AppLogger
    {
        private static readonly ILogger Log;

        static AppLogger()
        {
            var logPath = Path.Combine(
                HostingEnvironment.ApplicationPhysicalPath ?? AppDomain.CurrentDomain.BaseDirectory,
                "App_Data", "logs", "log-.txt");

            Log = new LoggerConfiguration()
                .MinimumLevel.Debug()
                .WriteTo.File(logPath,
                    rollingInterval: RollingInterval.Day,
                    retainedFileCountLimit: 30,
                    outputTemplate: "[{Timestamp:yyyy-MM-dd HH:mm:ss} {Level:u3}] {Message:lj}{NewLine}{Exception}")
                .CreateLogger();
        }

        public static void Info(string message) => Log.Information(message);

        public static void Warning(string message) => Log.Warning(message);

        public static void Error(string message, Exception ex = null) =>
            Log.Error(ex, message);
    }
}