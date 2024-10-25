using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Components;

namespace chdTour.App.Components.Base
{
    public abstract class BaseEditCollectionForm<TRepo, TParentRepo, T, TParent> : ComponentBase
        where TParentRepo : IBaseRepository<TParent>
        where TRepo : IBaseRepository<T>
        where T : class
        where TParent : class
    {
        [Inject] protected TRepo _repository { get; set; }

        [Parameter] public TParent ParentEntity { get; set; }
        [Parameter] public List<T> Removed { get; set; }
        [Parameter] public List<T> Added { get; set; }

        protected BaseEditCollectionInput<TParentRepo, T, TParent> _baseEditCollectionInput;

        protected override void OnAfterRender(bool firstRender)
        {
            if (firstRender)
            {
                if (this._baseEditCollectionInput is null) { throw new Exception(nameof(BaseEditCollectionInput<TParentRepo, T, TParent>) + " nicht zugewiesen"); }

                this._baseEditCollectionInput.AddedEntities = this.Added;
                this._baseEditCollectionInput.RemovedEntities = this.Removed;
            }
            base.OnAfterRender(firstRender);
        }
    }
}
