﻿namespace ArtUnion_API.Configs;

public static class AuthorizationConfig
{
    public static void AddAuthorizationConfiguration(this IServiceCollection services)
    {
        services.AddAuthorizationBuilder()
            .AddPolicy("EmailVerified", policy =>
            {
                policy.RequireClaim("email_verified", "true");
            });
    }
}