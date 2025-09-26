using Serilog;

namespace OfflineTicketing.Web.WebServices
{
    public class LogConfiguration
    {
        public static void ConfigureLogging(IHostBuilder builder)
        {
            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Error()
                .WriteTo.File(
                    "logs/log-.txt",
                    rollingInterval: RollingInterval.Day,
                    fileSizeLimitBytes: 2_000_000,
                    rollOnFileSizeLimit: true,
                    retainedFileCountLimit: 14,
                    shared: true
                )
                .CreateLogger();

            builder.UseSerilog();
        }
    }
}
