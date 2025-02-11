using Serilog.Configuration;
using Serilog;

namespace SymphonySolutions.Core.Logging
{
    public static class NUnitSinkExtensions
    {
        public static LoggerConfiguration NUnitOutput(
                  this LoggerSinkConfiguration loggerConfiguration,
                  IFormatProvider formatProvider = null)
        {
            return loggerConfiguration.Sink(new NUnitSink(formatProvider));
        }
    }
}
