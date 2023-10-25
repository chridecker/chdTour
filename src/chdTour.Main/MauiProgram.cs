using chdTour.BL.Extensions;
using chdTour.Persistence.EF;
using Microsoft.Extensions.Logging;
using MudBlazor.Services;

namespace chdTour.Main
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                });

            builder.Services.AddMauiBlazorWebView();

#if DEBUG
            builder.Services.AddBlazorWebViewDeveloperTools();
            builder.Logging.AddDebug();
#endif
            builder.Services.AddMudServices();
            builder.Services.AddchdTourBL(builder.Configuration);

            return builder.Build();
        }
    }
}