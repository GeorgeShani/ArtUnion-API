using Microsoft.AspNetCore.Mvc;
using System.Text.Json.Serialization;

namespace ArtUnion_API.Configs;

public static class JsonOptionsConfig
{
    public static void AddStringEnumConverter(this JsonOptions options)
    {
        options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    }
}