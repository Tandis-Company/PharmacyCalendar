using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;


namespace PharmacyCalendar.Infrastructure.Configuration
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddPersistance(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext(configuration);
            services.AddRepositories(configuration);
            return services;
        }
        public static IApplicationBuilder InitializeDatabase(this IApplicationBuilder app)
        {
            using (var scope = app.ApplicationServices.CreateScope())
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<PharmacyCalendarDbContext>();
                dbContext.Database.Migrate();
            }

            return app;
        }
        private static void AddDbContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<PharmacyCalendarDbContext>(options =>
            {
                options.UseSqlServer(configuration.GetConnectionString("ConnectionString"));
                options.EnableSensitiveDataLogging();
            });
        }
        private static void AddRepositories(this IServiceCollection services, IConfiguration configuration)
        {

        }
    }
}
