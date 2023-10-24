using chdTour.Contracts.Settings;
using chdTour.Persistence.EF;
using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chdTour.BL.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddchdTourBI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<chdTourContext>((sp, options) =>
            {
                options.UseSqlite(sp.GetRequiredService<IOptionsMonitor<DBSettings>>().CurrentValue.ConnectionString);
            });
            return services;
        }

        public static async Task<IApplicationBuilder> EnsureMigrationOfContext<T>(this IApplicationBuilder app) where T : DbContext
        {
            var context = app.ApplicationServices.GetService<T>();
            await context.Database.MigrateAsync();
            return app;
        }
    }
}
