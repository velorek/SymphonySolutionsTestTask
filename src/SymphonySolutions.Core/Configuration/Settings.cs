using System.ComponentModel.DataAnnotations;
using SymphonySolutions.Core.Configuration.Interfaces;

namespace SymphonySolutions.Core.Configuration;

public sealed class Settings : IBrowserSettings, IWebDriverSettings, IBaseTestSettings
{
    [Required(ErrorMessage = "Browser is required")] //TODO: check why not fired error
    public required string Browser { get; set; }

    [Required(ErrorMessage = "BaseHost is required")]
    public required string BaseHost { get; set; }
    public bool IsHeadless { get; set; } = true;
    public int DefaultTimeoutSeconds { get; set; } = 60;
    public int DefaultPollingIntervalMs { get; set; } = 200;

    public int MaxRetriesCount { get; set; } = 3;
    public int retriesInitialDelayMs { get; set; } = 200;
    public string TestResults { get; set; }

}
