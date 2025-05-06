using Hangfire;
using Hangfire.MemoryStorage;

namespace ArtUnion_API.Configs;

public static class HangfireConfig
{
    public static void AddHangfireConfiguration(this IServiceCollection services)
    {
        services.AddHangfire(config =>
        {
            config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseMemoryStorage();
        });
    }
}