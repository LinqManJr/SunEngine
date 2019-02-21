using Microsoft.Extensions.DependencyInjection;
using SunEngine.Managers;
using SunEngine.Scheduler;

namespace SunEngine.Configuration.AddServices
{
    public static class AddJobsExtensions
    {
        public static void AddJobs(this IServiceCollection services)
        {
            services.AddHostedService<CleanCacheJob>();
        }
    }
}