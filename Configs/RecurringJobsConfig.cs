using Hangfire;
using ArtUnion_API.Services.Interfaces;

namespace ArtUnion_API.Configs;

public static class RecurringJobsConfig
{
    public static void ConfigureRecurringJobs(this IApplicationBuilder app)
    {
        using var scope = app.ApplicationServices.CreateScope();
        var recurringJobManager = scope.ServiceProvider.GetRequiredService<IRecurringJobManager>();

        recurringJobManager.AddOrUpdate<IWeeklyDigestService>(
            "weekly-digest-job",
            service => service.SendWeeklyDigestAsync(),
            Cron.Weekly(DayOfWeek.Monday, 9)
        );
    }
}
