using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;

namespace chdTour.App.Components.Base
{
    public abstract class BaseEditAssignCollectionForm<TRepo, TParentRepo, T, TAssign, TParent> : BaseEditCollectionForm<TRepo, TParentRepo, T, TParent>
        where TParentRepo : IBaseRepository<TParent>
        where TRepo : IBaseRepository<T>
        where T : class
        where TParent : class
        where TAssign : class
    {


    }
}
