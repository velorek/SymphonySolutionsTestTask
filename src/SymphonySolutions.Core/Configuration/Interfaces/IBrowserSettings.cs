namespace SymphonySolutions.Core.Configuration.Interfaces
{
    public interface IBrowserSettings
    {
        string Browser { get; set; }
        bool IsHeadless { get; set; }
    }
}
