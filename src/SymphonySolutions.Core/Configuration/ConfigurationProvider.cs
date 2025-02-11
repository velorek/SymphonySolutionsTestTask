using Microsoft.Extensions.Configuration;

namespace SymphonySolutions.Core.Configuration
{
    public static class ConfigurationProvider
    {
        public static Settings LoadSettings()
        {
            var path = Directory.GetCurrentDirectory();
            var builder =
                new ConfigurationBuilder().SetBasePath(path);

            var configuration = builder
                .AddJsonFile("appsettings.json")
# if PROD
                .AddJsonFile("appsettings.PROD.json")
#endif
                .AddEnvironmentVariables()
                .Build();

            if(configuration is null)
            {
                throw new ArgumentNullException(nameof(configuration));
            }

            var settings = configuration.Get<Settings>() ?? throw new ArgumentNullException("Can't get settings");

            return settings;
        }
    }
}
