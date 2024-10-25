using Blazored.Modal.Services;
using chdTour.App.Components.Inputs;
using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Components;

namespace chdTour.App.Components.Base
{
    public abstract class BaseEditModalButton<TRepo, T> : ComponentBase
        where TRepo : IBaseRepository<T>
        where T: class
    {
        [CascadingParameter] protected EditFormLayout<TRepo, T> _parentLayout { get; set; } = default!;

        [Inject] protected TRepo _repository { get; set; }
        [Inject] protected IModalService _modal { get; set; }
        [Parameter] public string Title { get; set; }

        [Parameter] public string? FAIcon{get;set;}

        protected abstract Task Open();
    }
}
