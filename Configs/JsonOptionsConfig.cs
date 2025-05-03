using System.Text.Json.Serialization;

namespace ArtUnion_API.Configs;

public static class JsonOptionsConfig
{
    public static void AddCustomJsonOptions(this IServiceCollection services)
    {
        services.AddControllers().AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
        });
    }
}