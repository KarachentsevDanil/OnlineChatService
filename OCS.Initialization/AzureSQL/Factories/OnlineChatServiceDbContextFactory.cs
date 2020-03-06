using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using OCS.DAL.EF.Context;
using OCS.Initialization.AzureSQL.Builders;
using OCS.Initialization.AzureSQL.Constants;
using System.IO;
using OCS.Initialization.Infrastructure.Builders;

namespace OCS.Initialization.AzureSQL.Factories
{
    public class OnlineChatServiceDbContextFactory : IDesignTimeDbContextFactory<OnlineChatServiceDbContext>
    {
        public OnlineChatServiceDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot config = ConfigurationBuilderHelper.Create(Directory.GetCurrentDirectory());

            return DbContextBuilder.Build<OnlineChatServiceDbContext>(
                config,
                "OCS.DAL.EF.Migrations",
                AzureSqlConstants.AzureSqlConnectionKey);
        }
    }
}