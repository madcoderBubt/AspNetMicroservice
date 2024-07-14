using Microsoft.EntityFrameworkCore;
using Order.Infrastructure.Data.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using Microsoft.AspNetCore.Builder;

namespace Order.Infrastructure.Data.Extensions
{
    public static class HostExtension
    {
        public static IHost MigrateDatabase<TContext>(this IHost host, Action<TContext, IServiceProvider> seeder, int? retry = 0)
            where TContext : DbContext
        {
            int retryAvailable = retry.Value;

            using (var scope = host.Services.CreateScope())
            {
                var context = scope.ServiceProvider.GetService<TContext>();
                var logger = scope.ServiceProvider.GetRequiredService<ILogger<TContext>>();

                try
                {
                    logger.LogInformation("Seeding Started...");
                    InvokeSeeder(seeder, context, scope.ServiceProvider);
                    logger.LogInformation("Seeding Ended...");
                }
                catch (Exception ex)
                {
                    logger.LogError(ex.Message);
                    if(retryAvailable < 50)
                    {
                        retryAvailable++;
                        Thread.Sleep(2000);
                        MigrateDatabase<TContext>(host, seeder, retryAvailable);
                    }
                }
            }

            return host;
        }

        private static void InvokeSeeder<TContext>(Action<TContext, IServiceProvider> seeder, TContext? context, IServiceProvider serviceProvider) 
            where TContext : DbContext
        {
            context.Database.Migrate();
            seeder(context, serviceProvider);
        }
    }
}
