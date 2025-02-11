namespace SymphonySolutions.Core.Configuration.Interfaces
{
    public interface IWebDriverSettings : IBrowserSettings
    {
        int DefaultTimeoutSeconds { get; set; }
        int DefaultPollingIntervalMs { get; set; }
        int MaxRetriesCount { get; set; }
        int retriesInitialDelayMs { get; set; }
    }
}
