using Microsoft.Extensions.Configuration;
using OCS.Initialization.AzureSQL;
using OCS.Initialization.Infrastructure.Constants;

namespace OCS.Initialization.Infrastructure.Builders
{
    public static class ConfigurationBuilderHelper
    {
        public static IConfigurationRoot Create(
            string basePath, string settingFileName = InitializationConstants.SettingFileName)
        {
            IConfigurationBuilder builder = new ConfigurationBuilder()
                .SetBasePath(basePath)
                .AddJsonFile(settingFileName, optional: false, reloadOnChange: true)
                .AddEnvironmentVariables();

            return builder.Build();
        }
    }
}