using chd.UI.Base.Components.Base.Edit;
using chd.UI.Base.Contracts.Dtos.UltraGrid;
using chd.UI.Base.Contracts.Interfaces.Data;
using chdTour.DataAccess.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chdTour.App.Components.Pages.Base
{
    public abstract class BaseTourSerachForm<TRepo, T> : BaseSearchForm<TRepo, T, Guid>
        where TRepo : class, IBaseRepository<T, Guid>
        where T : class, IBaseEntity<Guid>
    {
        protected async Task Reload()
        {
            this._items = await this.Include(this._repository.Where(x => true)).ToListAsync();
            await this.InvokeAsync(this.StateHasChanged);
        }

        protected abstract IEnumerable<GridColumDto> Columns { get; }
    }
}
