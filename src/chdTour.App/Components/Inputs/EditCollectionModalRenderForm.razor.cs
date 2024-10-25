using chdTour.App.Components.Base;
using chdTour.DataAccess.Contracts.Domain.Base;
using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Components;

namespace chdTour.App.Components.Inputs
{
    public partial class EditCollectionModalRenderForm<TRepo, TParentRepo, T, TParent, TModal>
        where T : class
        where TRepo : IBaseRepository<T>
        where TParentRepo : IBaseRepository<TParent>
        where TParent : BaseEntity<Guid>
        where TModal : BaseEditCollectionForm<TRepo, TParentRepo, T, TParent>
    {

        [Parameter] public EditFormLayout<TParentRepo, TParent> ParentLayout { get; set; }

        [Parameter] public TParent ParentEntity { get; set; }
        [Parameter] public List<T> Added { get; set; }
        [Parameter] public List<T> Removed { get; set; }

        private IDictionary<string, object> _parameterDict => new Dictionary<string, object>
        {
            {nameof(BaseEditCollectionForm<TRepo,TParentRepo, T, TParent >.Removed),this.Removed},
            {nameof(BaseEditCollectionForm<TRepo,TParentRepo, T, TParent >.Added),this.Added},
            {nameof(BaseEditCollectionForm<TRepo,TParentRepo, T, TParent >.ParentEntity),this.ParentEntity},
        };
    }
}