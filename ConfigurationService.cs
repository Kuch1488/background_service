using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace background_service
{
    public class ConfigurationService
    {
        private readonly IConfiguration _configuration;

        public ConfigurationService()
        {
            var builder = new ConfigurationBuilder()
           .AddJsonFile("appsettings.json", optional: false, reloadOnChange: true);
#if DEBUG
            builder.AddJsonFile($"appsettings.Development.json", optional: true);
#endif
            _configuration = builder.Build();
        }

        public AppSettings GetAppSettings()
        {
            var appSettings = new AppSettings();
            _configuration.GetSection("AppSettings").Bind(appSettings);
            return appSettings;
        }
    }
}
