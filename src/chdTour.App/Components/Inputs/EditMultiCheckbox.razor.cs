using chd.UI.Base.Contracts.Interfaces.Services.Base;
using chdTour.App.Components.Base;
using chdTour.DataAccess.Contracts.Domain.Base;
using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace chdTour.App.Components.Inputs
{
    public partial class EditMultiCheckbox<TRepo, TParentRepo, T, TParent, TMultiAssign> : BaseEditCollectionInput<TParentRepo, T, TParent>
        where TParentRepo : IBaseRepository<TParent>
        where TRepo : IBaseRepository<T>
        where T : class
        where TParent : class
        where TMultiAssign : class
    {
        [Inject] TRepo _repository { get; set; }
        [Parameter] public IList<TMultiAssign> MultiAssignCollection { get; set; }

        [Parameter] public Expression<Func<T, TMultiAssign>> MultiAssignedProperty { get; set; }

        [Parameter] public Func<TMultiAssign, bool> MultiContains { get; set; }

        [Parameter] public RenderFragment<TMultiAssign> ElementContent { get; set; }

        private PropertyInfo _propertyInfoMultiAssign;

        protected override void OnInitialized()
        {
            var memberExpression = this.MultiAssignedProperty.Body as MemberExpression ?? throw new ArgumentException("The expression is not a member access expression.", nameof(this.MultiAssignedProperty));
            this._propertyInfoMultiAssign = memberExpression.Member as PropertyInfo ?? throw new ArgumentException("The member access expression does not access a property.", nameof(this.MultiAssignedProperty));
            base.OnInitialized();
        }

        private async Task ValueChanged(TMultiAssign? item, bool added)
        {
            if (added && !this.MultiContains(item))
            {
                var newEntity = Activator.CreateInstance<T>();
                if (newEntity is BaseEntity<Guid> entity)
                {
                    entity.Id = Guid.NewGuid();
                }
                this._propertyInfoOneAssign.SetValue(newEntity, this.ParentEntity);
                this._propertyInfoMultiAssign.SetValue(newEntity, item);
                await this._repository.SaveAsync(newEntity, this.Token);
                await this.ValueChanged(newEntity, EntityState.Added);
            }
            else if (!added && this.MultiContains(item))
            {
                var entity = this._entities.FirstOrDefault(x => this._propertyInfoMultiAssign.GetValue(x) == item);
                await this._repository.DeleteAsync(entity, this.Token);
                await this.ValueChanged(entity, EntityState.Deleted);
            }
        }
    }
}