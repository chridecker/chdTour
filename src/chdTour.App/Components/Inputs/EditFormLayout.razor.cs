using Blazored.Modal;
using Blazored.Modal.Services;
using chd.UI.Base.Components.Extensions;
using chd.UI.Base.Contracts.Enum;
using chdTour.DataAccess.Contracts.Domain.Base;
using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Routing;
using Microsoft.EntityFrameworkCore;
using Microsoft.JSInterop;

namespace chdTour.App.Components.Inputs
{
    public partial class EditFormLayout<TRepo, T> : ComponentBase, IDisposable
        where TRepo : IBaseRepository<T>
        where T : class
    {
        [CascadingParameter] public BlazoredModalInstance Modal { get; set; }

        [Inject] private IModalService _modal { get; set; }
        [Inject] private IJSRuntime _js { get; set; }
        [Inject] private TRepo _repository { get; set; }
        [Inject] private NavigationManager _navigationManager { get; set; }

        [Parameter] public T Value { get; set; }
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public EventCallback OnBack { get; set; }
        [Parameter] public CancellationToken Token { get; set; }

        private IDisposable _registerLocationChangeHandler;

        public event EventHandler<T> SaveInvoked;

        public Func<Task<bool>>? ValidateBeforeSave { get; set; } = null;

        public Func<T, Task> NewEntity { get; set; }

        protected override void OnInitialized()
        {
            this._registerLocationChangeHandler = this._navigationManager.RegisterLocationChangingHandler(OnLocationChanging);
            base.OnInitialized();
        }

        private async Task New()
        {
            await this.HandleNewEntity(Activator.CreateInstance<T>());
            await this.InvokeAsync(StateHasChanged);
        }

        private async Task Save()
        {
            if (this.ValidateBeforeSave is not null)
            {
                var valid = await this.ValidateBeforeSave();

                if (!valid) { return; }
            }

            if (this.Modal is not null)
            {
                await this.Modal.CloseAsync();
                return;
            }

            var modal = this._modal.ShowLoading();
            try
            {
                this.SaveInvoked?.Invoke(this, this.Value);
                await this._repository.SaveAsync(this.Value, this.Token);
            }
            catch (Exception ex)
            {
                await this._modal.ShowDialog(ex.Message, EDialogButtons.OK);
            }
            finally
            {
                modal.Close();
            }
        }

        private async Task Delete()
        {
            var modal = this._modal.ShowLoading();
            try
            {
                await this._repository.DeleteAsync(this.Value, this.Token);
            }
            catch (Exception ex)
            {
                await this._modal.ShowDialog(ex.Message, EDialogButtons.OK);
            }
            finally
            {
                modal.Close();
            }
        }

        private async Task HandleNewEntity(T entity)
        {
            if (entity is BaseEntity<Guid> baseEntity)
            {
                baseEntity.Id = Guid.NewGuid();
            }
            await this.NewEntity.Invoke(entity);
        }

        public async Task OpenChanges() => await this._modal.ShowDialog("Sie haben offene Änderungen.", EDialogButtons.OK);

        private object? GetId()
        {
            var prop = this.Value.GetType().GetProperty(nameof(BaseEntity<Guid>.Id));
            return prop?.GetValue(this.Value);
        }

        private async Task CopyId() => await this._js.InvokeVoidAsync("navigator.clipboard.writeText", this.GetId()?.ToString());

        private async ValueTask OnLocationChanging(LocationChangingContext context)
        {
            var res = await this.PreventNavigationForOpenChanges();
            if (res) { context.PreventNavigation(); }
        }

        private async Task<bool> PreventNavigationForOpenChanges()
        {
            var res = await this._modal.ShowDialog("Wollen Sie die Seite wirklich verlassen?", EDialogButtons.YesNo);
            if (res != EDialogResult.Yes) { return true; }
            return false;
        }

        public async Task InvokeStateChange() => await this.InvokeAsync(StateHasChanged);


        private async Task Back()
        {
            await this.OnBack.InvokeAsync();
        }

        public void Dispose()
        {
            this._registerLocationChangeHandler?.Dispose();
        }
    }
}