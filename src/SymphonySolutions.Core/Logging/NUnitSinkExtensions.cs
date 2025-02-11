using Serilog.Configuration;
using Serilog.Core;
using Serilog.Events;
using Serilog.Formatting.Display;
using Serilog;

namespace SymphonySolutions.Core.Logging
{
    public static class NUnitSinkExtensions
    {
        /*const string DefaultOutputTemplate = "{Timestamp:yyyy-MM-dd HH:mm:ss} [{Level}] {Message}{NewLine}  {Exception}";

        public static LoggerConfiguration NUnitOutput(
            this LoggerSinkConfiguration sinkConfiguration,
            LogEventLevel restrictedToMinimumLevel = LevelAlias.Minimum,
            IFormatProvider formatProvider = null,
            LoggingLevelSwitch levelSwitch = null,
            string outputTemplate = DefaultOutputTemplate)
        {
            if (sinkConfiguration == null)
            {
                throw new ArgumentNullException(nameof(sinkConfiguration));
            }

            var formatter = new MessageTemplateTextFormatter(outputTemplate, formatProvider);

            return sinkConfiguration.Sink(new NUnitSink(formatter), restrictedToMinimumLevel, levelSwitch);
        }*/
        public static LoggerConfiguration NUnitOutput(
                  this LoggerSinkConfiguration loggerConfiguration,
                  IFormatProvider formatProvider = null)
        {
            return loggerConfiguration.Sink(new NUnitSink(formatProvider));
        }
    }
}
