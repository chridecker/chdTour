using Blazored.Modal.Services;
using chd.UI.Base.Contracts.Dtos.UltraGrid;
using chd.UI.Base.Contracts.Interfaces.Data;
using chdTour.App.Components.Layout;
using chdTour.DataAccess.Contracts.Domain.Base;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace chdTour.App.Components.Pages
{
    public partial class EntitySearch<TRepo, T, PK> : ComponentBase
        where TRepo : class, IBaseRepository<T, PK>
        where T : class, IBaseEntity<PK>
        where PK : struct

    {
        private bool _isItemVisible => this.Item is not null;

        [Parameter] public bool IsGroupable { get; set; }
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter, EditorRequired] public IEnumerable<GridColumDto> Columns { get; set; }
        [Parameter, EditorRequired] public IEnumerable<T> Items { get; set; }
        [Parameter] public EventCallback<T> OnEdit { get; set; }
        [Parameter] public EventCallback OnReload{ get; set; }

        [Parameter] public T? Item { get; set; }
        private async Task CreateEntity()
        {
            var newEntity = Activator.CreateInstance<T>();
            this.HandleNewEntity(newEntity);

            await this.OnEdit.InvokeAsync(newEntity);
        }
        private void HandleNewEntity(T entity)
        {
            if (entity is BaseEntity<Guid> dioEntity)
            {
                dioEntity.Id = Guid.NewGuid();
            }
        }
    }
}