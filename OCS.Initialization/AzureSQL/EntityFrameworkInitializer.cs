using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OCS.Initialization.AzureSQL.Builders;
using OCS.Initialization.AzureSQL.Constants;
using System;
using System.Threading.Tasks;

namespace OCS.Initialization.AzureSQL
{
    public static class EntityFrameworkInitializer
    {
        public static async Task InitializeAsync<T>(
            IConfigurationRoot config,
            string migrationPath,
            string connectionKey = AzureSqlConstants.AzureSqlConnectionKey)
            where T : DbContext
        {
            Console.WriteLine("Start migration...");

            try
            {
                using (T context = DbContextBuilder.Build<T>(config, migrationPath, connectionKey))
                {
                    await context.Database.MigrateAsync();
                    await context.Database.EnsureCreatedAsync();
                }

                Console.WriteLine("End migration...");
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
