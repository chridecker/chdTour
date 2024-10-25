using chd.UI.Base.Components.Base;
using chdTour.App.Components.Base;
using chdTour.Contracts.Constants;
using chdTour.DataAccess.Contracts.Domain;
using chdTour.DataAccess.Contracts.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;

namespace chdTour.App.Components.Pages
{
    public partial class ScalaPage : BaseSearchForm<IGradeScalaRepository, GradeScala>
    {
        protected override async Task OnInitializedAsync()
        {
            this.Title = PageTitleConstants.Skala;

            this._items = (await this._repository.FindAll(this._cts.Token)).ToList();

            await base.OnInitializedAsync();
        }

    }
}