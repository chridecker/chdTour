using Blazored.Modal.Services;
using chdTour.App.Components.Inputs;
using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Components;

namespace chdTour.App.Components.Base
{
    public abstract class BaseEditForm<TRepo, T> : ComponentBase
        where TRepo : IBaseRepository<T>
        where T : class
    {
        [Inject] protected TRepo _repository { get; set; }
        [Inject] protected IModalService _modal { get; set; }

        [Parameter] public T Value { get; set; }
        [Parameter] public EventCallback OnBack { get; set; }

        protected EditFormLayout<TRepo, T> _layout;


        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (this._layout is null) { throw new Exception(nameof(EditFormLayout<TRepo, T>) + " nicht zugewiesen"); }

                this._layout.NewEntity = this.NewEntity;

                this._layout.ValidateBeforeSave = this.ValidateBeforeSave;
            }

            await base.OnAfterRenderAsync(firstRender);
        }

        private async Task NewEntity(T entity)
        {
            this.Value = entity;
            await this.InvokeAsync(this.StateHasChanged);
        }

        protected virtual Task<bool> ValidateBeforeSave() => Task.FromResult(true);
    }
}
