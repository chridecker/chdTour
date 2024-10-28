using chd.UI.Base.Client.Extensions;
using chd.UI.Base.Client.Implementations.Services;
using chd.UI.Base.Contracts.Interfaces.Authentication;
using chd.UI.Base.Contracts.Interfaces.Services;
using chd.UI.Base.Contracts.Interfaces.Update;
using chdTour.App.Implementations;
using chdTour.BL.Extensions;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
namespace chdTour.App.Extensions
{
    public static class DIExtensions
    {
        public static IServiceCollection AddUI(this IServiceCollection services, IConfiguration configuration, ServiceLifetime serviceLifetime = ServiceLifetime.Singleton)
        {
            services.AddAuthorizationCore();

            services.AddUtilities<chdTourProfileService, int, int, HandleUserIdLogin, SettingManager, ISettingManager, UiHandler, IBaseUIComponentHandler, UpdateService>(serviceLifetime);


            services.AddMauiModalHandler();

            services.AddTransient<IDownloadService, DownloadService>();
            services.AddTransient<ICustomFilePicker, CustomFilePicker>();

            services.AddSingleton<IVibrationHelper, VibrationHelper>();

            services.AddSingleton<IAppInfoService, AppInfoService>();

            services.AddSingleton<IchdTourProfileService>(sp => sp.GetRequiredService<chdTourProfileService>());

            /* State Container Singletons */
            services.AddSingleton<INavigationHistoryStateContainer, NavigationHistoryStateContainer>();

            /* Scoped */
            services.AddScoped<INavigationHandler, NavigationHandler>();

            services.AddchdTourBL(configuration);



            return services;
        }
    }
}
