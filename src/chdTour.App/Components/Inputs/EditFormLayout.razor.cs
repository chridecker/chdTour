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
    public partial class EditFormLayout<TRepo, T> : ComponentBase
        where TRepo : IBaseRepository<T>
        where T : class
    {
        [CascadingParameter] public BlazoredModalInstance Modal { get; set; }

        [Inject] private IModalService _modal { get; set; }
        [Inject] private TRepo _repository { get; set; }

        [Parameter] public T Value { get; set; }
        [Parameter] public RenderFragment? ChildContent { get; set; }
        [Parameter] public EventCallback OnBack { get; set; }
        [Parameter] public CancellationToken Token { get; set; }
        [Parameter] public bool IsNew { get; set; }

        private IDisposable _registerLocationChangeHandler;

        public Func<Task<bool>>? ValidateBeforeSave { get; set; } = null;

        public Func<T, Task> NewEntity { get; set; }


        private async Task Save()
        {
            if (this.ValidateBeforeSave is not null)
            {
                var valid = await this.ValidateBeforeSave();

                if (!valid) { return; }
            }
            if (this.Modal is not null)
            {
                await this.Modal.CloseAsync(ModalResult.Ok());
                return;
            }
            var modal = this._modal.ShowLoading();
            try
            {
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

        public async Task InvokeStateChange() => await this.InvokeAsync(StateHasChanged);


        private async Task Back()
        {
            await this.OnBack.InvokeAsync();
        }
    }
}