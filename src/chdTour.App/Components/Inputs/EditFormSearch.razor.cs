using Blazored.Modal;
using Blazored.Modal.Services;
using chdTour.App.Components.Base;
using chd.UI.Base.Components.General.Search;
using Microsoft.AspNetCore.Components;
using chd.UI.Base.Contracts.Interfaces.Services.Base;
using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;

namespace chdTour.App.Components.Inputs
{
    public partial class EditFormSearch<TRepo, T, TValue, TFavoriteObject> : BaseEditFormInput<TRepo, T, TValue>
        where TRepo : IBaseRepository<T>
        where T : class
    {
        [Inject] private IModalService _modal { get; set; }

        [Parameter] public IEnumerable<TValue> Values { get; set; }
        [Parameter] public Func<TValue?, string> Name { get; set; }
        [Parameter] public Func<TValue?, string>? Sub { get; set; } = null;
        [Parameter] public string? FavoriteKey { get; set; } = null;
        [Parameter] public Func<TValue?, TFavoriteObject>? FavoriteObject { get; set; } = null;

        private async Task Search()
        {
            if (this.Disabled) { return; }

            var parameters = new ModalParameters
            {
                { nameof(SearchModalComponent<TValue, TFavoriteObject>.Items), this.Values ?? [] },
                { nameof(SearchModalComponent<TValue, TFavoriteObject>.Name), this.Name },
                { nameof(SearchModalComponent<TValue, TFavoriteObject>.Sub), this.Sub },
                { nameof(SearchModalComponent<TValue, TFavoriteObject>.FavoriteKey), this.FavoriteKey },
                { nameof(SearchModalComponent<TValue, TFavoriteObject>.FavoriteObject), this.FavoriteObject },
            };

            var res = await this._modal.Show<SearchModalComponent<TValue, TFavoriteObject>>("Auswahl", parameters).Result;

            if (res.Cancelled || res.Data is not TValue value) { return; }

            await this.ValueChanged(value);
        }
    }
}