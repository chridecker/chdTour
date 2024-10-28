using Blazored.Modal.Services;
using chd.UI.Base.Components.Base;
using chd.UI.Base.Components.Extensions;
using chdTour.DataAccess.Contracts.Domain.Base;
using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace chdTour.App.Components.Base
{
    public abstract class BaseSearchForm<TRepo, T> : PageComponentBase<int, int>, IDisposable
        where TRepo : IBaseRepository<T>
        where T : BaseEntity<Guid>
    {
        [Inject] protected IModalService _modal { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }
        [Inject] protected TRepo _repository { get; set; }

        protected T? _item;
        protected IEnumerable<T> _items;
        protected CancellationTokenSource _cts = new CancellationTokenSource();

        protected override async Task OnInitializedAsync()
        {
            await this.LoadEntities();
            await base.OnInitializedAsync();
        }

        protected virtual Task<T> LoadEntity(T entity) => this._repository.FirstOrDefaultAsync(x => x.Id == entity.Id);

        protected virtual IQueryable<T> Include(IQueryable<T> queryable) => queryable;

        private async Task LoadEntities()
        {
            this._items = await this.Include(this._repository.Where(x => true)).AsSplitQuery().ToListAsync(this._cts.Token);
        }


        protected async Task CreateEntity()
        {
            var newEntity = Activator.CreateInstance<T>();
            this.HandleNewEntity(newEntity);
            this._item = newEntity;
            await this.InvokeAsync(this.StateHasChanged);
        }

        protected async Task SetEntity(T entity)
        {
            this._item = entity;
            await this.InvokeAsync(this.StateHasChanged);
        }

        protected async Task OnBack()
        {
            this._item = null;
            await this.LoadEntities();
            await this.InvokeAsync(this.StateHasChanged);
        }

        private void HandleNewEntity(T entity)
        {
            if (entity is BaseEntity<Guid> baseEntity)
            {
                baseEntity.Id = Guid.NewGuid();
            }
        }
        public void Dispose()
        {
            this._cts.Cancel();
            this._cts.Dispose();
        }

    }
}
