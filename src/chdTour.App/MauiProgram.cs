using Blazored.Modal;
using chdTour.App.Extensions;
using chdTour.Contracts.Constants;
using chdTour.Contracts.Settings;
using chdTour.Persistence.EF;
using CommunityToolkit.Maui;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Reflection;

namespace chdTour.App
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .UseMauiCommunityToolkit()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Configuration.AddConfiguration(GetAppSettingsConfig());
            builder.Configuration.AddConfiguration(GetLocalSetting());

            builder.AddServices();

            var app =  builder.Build();


            app.Services.GetService<chdTourContext>().Database.Migrate();

            return app;
        }
        private static IConfiguration GetAppSettingsConfig()
        {
            var fileName = "appsettings.json";
            var appSettingsFileName = "chdTour.App.appsettings.json";
            var assembly = Assembly.GetExecutingAssembly();
            using var resStream = assembly.GetManifestResourceStream(appSettingsFileName);
            if (resStream == null)
            {
                throw new ApplicationException($"Unable to read file [{appSettingsFileName}]");
            }
            return new ConfigurationBuilder()
                    .AddJsonStream(resStream)
                    .Build();
        }

        private static IConfiguration GetLocalSetting()
        {
            if (Preferences.ContainsKey(SettingConstants.DBFile))
            {
                var pref = Preferences.Default.Get<string>(SettingConstants.DBFile, string.Empty);
                var dict = new Dictionary<string, string>()
                {
                    //{$"{nameof(DBSettings)}:{nameof(DBSettings.ConnectionString)}",pref }
                };
                return new ConfigurationBuilder().AddInMemoryCollection(dict).Build();
            }
            return new ConfigurationBuilder().Build();
        }
        private static void AddServices(this MauiAppBuilder builder)
        {
            builder.Services.AddMauiBlazorWebView();
            builder.Services.AddBlazoredModal();
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Services.AddUI(builder.Configuration);
        }
    }
}
