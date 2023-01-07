using HchApiPlatform.DbContexts;
using HchApiPlatform.Options;
using HchApiPlatform.QuartzJobs;
using Microsoft.EntityFrameworkCore;
using NLog.Extensions.Logging;
using Oracle.ManagedDataAccess.Client;
using Quartz;

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

            OracleConfiguration.OracleDataSources.Add(DatabaseOptions.UnimaxHO, DbOptions.Unimax.Ho);
            OracleConfiguration.OracleDataSources.Add(DatabaseOptions.UnimaxHI, DbOptions.Unimax.Hi);

            services.AddDbContextFactory<UnimaxHoContext>(options => {
                options.UseOracle($"User Id={DbOptions.Unimax.UserID}; Password={DbOptions.Unimax.Password}; Data Source={DatabaseOptions.UnimaxHO};", opt => opt.UseOracleSQLCompatibility("11"));
            });
            services.AddDbContextFactory<UnimaxHiContext>(options => {
                options.UseOracle($"User Id={DbOptions.Unimax.UserID}; Password={DbOptions.Unimax.Password}; Data Source={DatabaseOptions.UnimaxHI};", opt => opt.UseOracleSQLCompatibility("11"));
            });

            services.AddDbContextFactory<HchPlatformContext>(options =>
            {
                options.UseSqlServer($"{DbOptions.Platform.Connection}");
            });

            return services;
        }

        public static IServiceCollection AddQuartzJobs(this IServiceCollection services, IConfiguration config)
        {
            var QzOptions = config.GetSection(Options.QuartzOptions.Quartz).Get<Options.QuartzOptions>();

            services.AddQuartz(q =>
            {
                q.UseMicrosoftDependencyInjectionJobFactory();
                q.ScheduleJob<UpdateAdmitBedStatsJob>(t =>
                    t.WithIdentity("Trigger_UpdateAdmitBedStatsJob")
                     .WithCronSchedule(QzOptions.CronSchedule, c =>
                        c.WithMisfireHandlingInstructionFireAndProceed())
                     .WithDescription($"Trigger UpdateAdmitBedStatsJob by Cron Schedule ({QzOptions.CronSchedule})"));
            });
            
            services.AddQuartzHostedService(q =>
                q.WaitForJobsToComplete = true);
            
            return services;
        }

        public static IServiceCollection AddNLog(this IServiceCollection services, IConfiguration config)
        {
            var NLogOpts = config.GetSection("NLog");

            services.AddLogging(log =>
            {
                log.ClearProviders();
                log.AddNLog(NLogOpts);
            });

            return services;
        }
    }
}
