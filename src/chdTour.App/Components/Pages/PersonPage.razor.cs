using chd.UI.Base.Components.Base;
using chdTour.App.Components.Base;
using chdTour.Contracts.Constants;
using chdTour.DataAccess.Contracts.Domain;
using chdTour.DataAccess.Contracts.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace chdTour.App.Components.Pages
{
    public partial class PersonPage : BaseSearchForm<IPersonRepository, DataAccess.Contracts.Domain.Person>
    {
        protected override async Task OnInitializedAsync()
        {
            this.Title = PageTitleConstants.Persons;

            this._items = await this._repository.Where(x => true).ToListAsync(this._cts.Token);

            await base.OnInitializedAsync();
        }

    }
}