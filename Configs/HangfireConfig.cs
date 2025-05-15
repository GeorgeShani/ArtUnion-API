using Hangfire;
using Hangfire.SqlServer;

namespace ArtUnion_API.Configs;

public static class HangfireConfig
{
    private static readonly string DatabaseUsername = Environment.GetEnvironmentVariable("AWS_RDS_DB_USERNAME")!;
    private static readonly string DatabasePassword = Environment.GetEnvironmentVariable("AWS_RDS_DB_PASSWORD")!;
    
    public static void AddHangfireConfiguration(this IServiceCollection services)
    {
        var connectionString = $"""
            Server=artunion-database.cvuu4wuuidsg.eu-north-1.rds.amazonaws.com;
            Database=ArtUnion;
            User Id={DatabaseUsername};
            Password={DatabasePassword};
            TrustServerCertificate=True;
        """;
        
        services.AddHangfire(config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(connectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.FromSeconds(15),
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                });
        });
        
        services.AddHangfireServer();
    }
}