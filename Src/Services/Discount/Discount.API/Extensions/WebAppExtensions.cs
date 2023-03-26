using Microsoft.Extensions.Hosting;
using Npgsql;

namespace Discount.API.Extensions;

public static class WebAppExtensions
{
    public static WebApplication MigrateDatabase<TContext>(this WebApplication application, int retry = 0)
    {
        int retryCount = retry;
        using (var scope = application.Services.CreateScope())
        {
            var services = scope.ServiceProvider;
            var configuration = services.GetRequiredService<IConfiguration>();
            var logger = services.GetRequiredService<ILogger<TContext>>();
            try
            {
                logger.LogInformation("Migrating postresql database.");
                using var conn = new NpgsqlConnection(configuration.GetValue<string>("PostgreDbSetting:ConnectionString"));
                conn.Open();
                using (var cmd = conn.CreateCommand()) {
                    cmd.CommandText = "DROP TABLE IF EXISTS Coupon";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = @"CREATE TABLE Coupon(Id SERIAL PRIMARY KEY, 
                                                                ProductName VARCHAR(24) NOT NULL,
                                                                Description TEXT,
                                                                Amount INT)";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('IPhone X', 'IPhone Discount', 150);";
                    cmd.ExecuteNonQuery();

                    cmd.CommandText = "INSERT INTO Coupon(ProductName, Description, Amount) VALUES('Samsung 10', 'Samsung Discount', 100);";
                    cmd.ExecuteNonQuery();
                }
                conn.Close();
                logger.LogInformation("Migrated postresql database.");
            }
            catch (NpgsqlException ex)
            {
                logger.LogError(ex, "An error occurred while migrating the postresql database");
                if (retryCount < 50)
                {
                    retryCount++;
                    System.Threading.Thread.Sleep(2000);
                    MigrateDatabase<TContext>(application, retryCount);
                }
            }
        }
        return application;
    }
}
