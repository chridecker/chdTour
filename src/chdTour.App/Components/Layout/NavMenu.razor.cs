using Blazored.Toast.Services;
using chd.UI.Base.Contracts.Dtos.Authentication;
using chd.UI.Base.Contracts.Interfaces.Authentication;
using chd.UI.Base.Contracts.Interfaces.Services;
using chdTour.Contracts.Constants;
using Microsoft.AspNetCore.Components;

namespace chdTour.App.Components.Layout
{
    public partial class NavMenu : ComponentBase
    {
        [Inject] private IBaseUIComponentHandler _uiHandler { get; set; }
        [Inject] private NavigationManager _navManager { get; set; }
        [Inject] private INavigationHandler _navigationHandler { get; set; }
        [Inject] private IServiceProvider _serviceProvider { get; set; }

        private bool _small = true;

        private bool collapseNavMenu = true;

        private string NavMenuCssClass => collapseNavMenu ? "collapse" : null;


        protected override async Task OnAfterRenderAsync(bool first)
        {
            await this.CheckWindowSize();

            await base.OnAfterRenderAsync(first);
        }

        private void GoHome()
        {
            this._navigationHandler.NavigateToRoot();
        }

        private async Task CheckWindowSize()
        {
            var dimension = await this._uiHandler.GetWindowDimensions();
            this._small = dimension.Width > 641;
        }

    }
}