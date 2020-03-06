using Microsoft.Extensions.Configuration;
using OCS.DAL.EF.Context;
using OCS.Initialization.AzureSQL;
using OCS.Initialization.Infrastructure.Builders;
using System.IO;
using System.Threading.Tasks;

namespace OCS.Initialization
{
    public static class Program
    {
        public static async Task Main()
        {
            IConfigurationRoot configuration = ConfigurationBuilderHelper.Create(Directory.GetCurrentDirectory());

            await EntityFrameworkInitializer.InitializeAsync<OnlineChatServiceDbContext>(configuration, "OCS.DAL.EF.Migrations");
        }
    }
}   