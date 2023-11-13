using System;
using Arival.Domain.Service;
using Arival.Infrastructure.Persistance;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Arival.Domain.Service;
using Arival.Infrastructure.Persistance.Repository;
using System.Reflection;


namespace Framework.DependencyConfig
{
    public class ApplicationConfig
    {
        public static ServiceProvider DoConfig(IServiceCollection services, IConfiguration configuration)
        {
            var connStr = configuration.GetConnectionString("ArivalConnection");
            //services.AddDbContext<ArivalDbContext>(o => o.UseNpgsql(connStr));
            var env = configuration.GetValue<string>("Environment");
            if(env.Equals("test",StringComparison.InvariantCultureIgnoreCase))
                services.AddDbContext<ArivalDbContext>(options => options.UseInMemoryDatabase(databaseName: "Arival"));
            else
                services.AddDbContext<ArivalDbContext>(o => o.UseNpgsql(connStr));

            services.AddSingleton<IConfiguration>(configuration);
            AppContext.SetSwitch("Npgsql.EnableLegacyTimestampBehavior", true);
 
            services.Scan(scan => scan
                .FromAssemblies(typeof(UserOtpRepository).GetTypeInfo().Assembly)
                .AddClasses()
                .AsImplementedInterfaces()
                .WithScopedLifetime());

            services.Scan(scan => scan
                .FromAssemblies(typeof(UserOtpService).GetTypeInfo().Assembly)
                .AddClasses()
                .AsImplementedInterfaces()
                .WithScopedLifetime());

           return services.BuildServiceProvider();
        }
    }
}
