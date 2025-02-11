using NUnit.Framework;
using Serilog.Core;
using Serilog.Events;

namespace SymphonySolutions.Core.Logging
{
    public class NUnitSink : ILogEventSink
    {
        private readonly IFormatProvider _formatProvider;

        public NUnitSink(IFormatProvider formatProvider)
        {
            _formatProvider = formatProvider;
        }

        public void Emit(LogEvent logEvent)
        {
            ArgumentNullException.ThrowIfNull(logEvent);

            if (TestContext.Out != null)
            {
                var message = logEvent.RenderMessage(_formatProvider);
                TestContext.Progress.WriteLine(message);
            }
        }
    }
}
