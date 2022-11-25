using HchApiPlatform.DbContexts;
using HchApiPlatform.Options;
using Microsoft.EntityFrameworkCore;
using Oracle.ManagedDataAccess.Client;

namespace HchApiPlatform.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddConfig(this IServiceCollection services, IConfiguration config)
        {
            services.Configure<DatabaseOptions>(config.GetSection(DatabaseOptions.Database));

            return services;
        }

        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            var DbOptions = config.GetSection(DatabaseOptions.Database).Get<DatabaseOptions>();

            OracleConfiguration.OracleDataSources.Add(DatabaseOptions.UnimaxHO, DbOptions.UnimaxHoTns);
            OracleConfiguration.OracleDataSources.Add(DatabaseOptions.UnimaxHI, DbOptions.UnimaxHiTns);

            services.AddDbContextFactory<UnimaxHoContext>(options => {
                options.UseOracle($"User Id={DbOptions.UserID}; Password={DbOptions.Password}; Data Source={DatabaseOptions.UnimaxHO};", opt => opt.UseOracleSQLCompatibility("11"));
            });
            services.AddDbContextFactory<UnimaxHiContext>(options => {
                options.UseOracle($"User Id={DbOptions.UserID}; Password={DbOptions.Password}; Data Source={DatabaseOptions.UnimaxHI};", opt => opt.UseOracleSQLCompatibility("11"));
            });

            return services;
        }
    }
}
