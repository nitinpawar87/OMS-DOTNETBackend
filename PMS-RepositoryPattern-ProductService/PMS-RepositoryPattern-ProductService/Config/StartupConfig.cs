using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using PMS_RepositoryPattern.Repository;
using PMS_RepositoryPattern.Service;
//using MongoDbClient;
//using UserManagement.Persistence;
//using UserManagement.Service;

namespace PMS_RepositoryPattern.Config
{
    public static class StartupConfig
    {
        public static ConfigurationSetting RegisterConfiguration(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<ConfigurationSetting>(configuration.GetSection("Configuration"));
            var serviceProvider = services.BuildServiceProvider();
            var iop = serviceProvider.GetService<IOptions<ConfigurationSetting>>();
            return iop.Value;
        }

        public static void RegisterServiceDependancies(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton<IProductServiceV1, ProductServiceV1>();
            services.AddSingleton<IProductServiceV2, ProductServiceV2>();
            services.AddSingleton<IProductRepository, ProductRepository>();
            //services.AddSingleton<IMongoDbQueryRepository, MongoDbQueryRepository>();
        }

        public static void RegisterDbDependancies(this IServiceCollection services, ConfigurationSetting configurationSetting)
        {
            //services.AddSingleton<IDatabaseContext>(new DatabaseContext(
            //    configurationSetting.DatabaseSettings.ConnectionString,
            //    configurationSetting.DatabaseSettings.DatabaseName));
        }
    }
}
