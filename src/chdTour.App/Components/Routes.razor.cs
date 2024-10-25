using chd.UI.Base.Contracts.Interfaces.Authentication;
using chd.UI.Base.Contracts.Interfaces.Services;
using chdTour.App.Implementations;
using chdTour.Contracts.Constants;
using Microsoft.AspNetCore.Components;

namespace chdTour.App.Components
{
    public partial class Routes
    {
        [Inject] IVibrationHelper _vibrationHelper { get; set; }
        [Inject] IBaseUIComponentHandler _baseUIComponentHandler { get; set; }



        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                var darkMode = await this._baseUIComponentHandler.GetDarkMode();
                await this._baseUIComponentHandler.SetDarkMode(darkMode);
            }
            await base.OnAfterRenderAsync(firstRender);
        }
        private async Task HandleError()
        {
            await this._vibrationHelper.Vibrate(3, TimeSpan.FromMilliseconds(300));
        }
    }
}