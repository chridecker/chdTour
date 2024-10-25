using Blazored.Modal.Services;
using chdTour.App.Components.Layout;
using chdTour.DataAccess.Contracts.Domain.Base;
using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Components;
using System.Reflection;

namespace chdTour.App.Components.Pages
{
    public partial class EntityTable<TRepo, T> : ComponentBase
        where TRepo : IBaseRepository<T>
        where T : class

    {
        private bool _isItemVisible => this.Item is not null;

        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter, EditorRequired] public IEnumerable<T> Items { get; set; }
        [Parameter] public EventCallback<T> OnEdit { get; set; }

        [Parameter] public RenderFragment? HeaderContent { get; set; }
        [Parameter] public RenderFragment<T>? RowTemplate { get; set; }

        [Parameter] public T? Item { get; set; }
        [Parameter] public string EntityName {get;set;}

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