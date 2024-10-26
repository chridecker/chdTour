using chdTour.Contracts.Settings;
using chdTour.DataAccess.Contracts.Interfaces.Repositories;
using chdTour.DataAccess.Repositories;
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
        public static IServiceCollection AddchdTourBL(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<chdTourContext>((sp, options) =>
            {
                var path = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "chdTour");
               if (!Directory.Exists(path))
                {
                    Directory.CreateDirectory(path);
                }

                options.UseSqlite($"Data Source={Path.Combine(path, $"{nameof(chdTourContext)}.db")}");
            });

            services.AddScoped<IPersonRepository, PersonRepository>();
            services.AddScoped<ITourRepository, TourRepository>();
            services.AddScoped<ITourTypeRepository, TourTypeRepository>();
            services.AddScoped<IGradeRepository, GradeRepository>();
            services.AddScoped<IGradeScalaRepository, GradeScalaRepository>();

            return services;
        }
    }
}
