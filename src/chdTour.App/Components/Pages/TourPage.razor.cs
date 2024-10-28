using chd.UI.Base.Components.Base;
using chdTour.App.Components.Base;
using chdTour.Contracts.Constants;
using chdTour.DataAccess.Contracts.Domain;
using chdTour.DataAccess.Contracts.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace chdTour.App.Components.Pages
{
    public partial class TourPage : BaseSearchForm<ITourRepository, Tour>
    {
        protected override async Task OnInitializedAsync()
        {
            this.Title = PageTitleConstants.Tours;
            await base.OnInitializedAsync();
        }

        

    }
}