using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Components;

namespace chdTour.App.Components.Base
{
    public class BaseEditPartialDataForm<TRepo, T> : ComponentBase
        where TRepo : IBaseRepository<T>
        where T : class
    {
        [Inject] protected TRepo repository { get; set; }
        [Parameter] public T Value { get; set; }
    }
}
