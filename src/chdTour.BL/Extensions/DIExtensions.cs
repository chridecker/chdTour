using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chdTour.BL.Extensions
{
    public static  class DIExtensions
    {
        public static IServiceCollection AddchdTourBI(this IServiceCollection services, IConfiguration configuration)
        {

            //services.AddDbContext
            return services;
        }
    }
}
