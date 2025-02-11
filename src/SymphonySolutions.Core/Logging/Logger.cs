using Serilog;

namespace SymphonySolutions.Core.Logging
{
    public class Logger
    {
        private readonly string _name;
        private const string messageTemplate = "[{Date}] - [{Module}] - [{Type}]: {Message}";
        public Logger(string name)
        {
            _name = name;
        }

        public static void InitLogger()
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.NUnitOutput()
                .CreateLogger();
        }

        public void Info(string message) => 
            Log.Information(messageTemplate, DateTimeOffset.Now, _name, "INFO", message);

        public void Warn(string message) =>
            Log.Warning(messageTemplate, DateTimeOffset.Now, _name, "WARN", message);

        public void Error(string message, Exception exception) =>
            Log.Error(exception, messageTemplate, DateTimeOffset.Now, _name, "ERROR", message);

        public void Error(string message) =>
            Log.Error(messageTemplate, DateTimeOffset.Now, _name, "ERROR", message);
    }
}
