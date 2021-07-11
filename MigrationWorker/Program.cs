using System;
using Database;
using Config;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace MigrationWorker
{
    class Program
    {
        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    
                    services.AddDbContext<ApplicationDataContext>(options =>
                    {
                        options.UseNpgsql(
                            Config.Config.DbConnectionString,
                            b => b.MigrationsAssembly(nameof(MigrationWorker))
                        ).UseLoggerFactory(LoggerFactory.Create(builder =>
                        {
                            builder
                                .AddConsole((_) => { })
                                .AddFilter((category, level) =>
                                    category == DbLoggerCategory.Database.Command.Name
                                    && level == LogLevel.Information);
                        }));
                        services.AddHostedService<Worker>();
                    });

                });
    }
}