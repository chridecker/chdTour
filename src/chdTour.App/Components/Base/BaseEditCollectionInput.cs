using chdTour.App.Components.Inputs;
using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;
using System.Reflection;

namespace chdTour.App.Components.Base
{
    public class BaseEditCollectionInput<TParentRepo, T, TParent> : ComponentBase, IDisposable
        where TParentRepo : IBaseRepository<TParent>
        where TParent : class
        where T : class
    {
        [Inject] protected TParentRepo _parentRepository { get; set; }

        [CascadingParameter] private EditFormLayout<TParentRepo, TParent> _parentLayout { get; set; } = default!;

        [Parameter] public Expression<Func<TParent, ICollection<T>>> CollectionProperty { get; set; }

        [Parameter] public Expression<Func<T, object>> OneAssignedProperty { get; set; }

        [Parameter] public TParent ParentEntity { get; set; }

        [Parameter] public string? Title { get; set; }
        [Parameter] public CancellationToken Token { get; set; }

        protected ICollection<T> _entities => (ICollection<T>?)this._propertyInfoCollection.GetValue(this.ParentEntity);
        protected PropertyInfo _propertyInfoOneAssign;

        private PropertyInfo _propertyInfoCollection;

        public List<T> RemovedEntities { get; set; } = [];
        public List<T> AddedEntities { get; set; } = [];


        protected override void OnInitialized()
        {
            this._parentLayout.SaveInvoked += this._parentLayout_SaveInvoked;

            var memberExpressionCollection = this.CollectionProperty.Body as MemberExpression ?? throw new ArgumentException("The expression is not a member access expression.", nameof(this.CollectionProperty));
            var memberExpression = this.OneAssignedProperty.Body as MemberExpression ?? throw new ArgumentException("The expression is not a member access expression.", nameof(this.OneAssignedProperty));
            this._propertyInfoCollection = memberExpressionCollection.Member as PropertyInfo ?? throw new ArgumentException("The member access expression does not access a property.", nameof(this.CollectionProperty));
            this._propertyInfoOneAssign = memberExpression.Member as PropertyInfo ?? throw new ArgumentException("The member access expression does not access a property.", nameof(this.OneAssignedProperty));

            if (!typeof(TParent).IsAssignableTo(this._propertyInfoOneAssign.PropertyType))
            {
                throw new Exception($"TParent ist kein Typ von {this._propertyInfoOneAssign.PropertyType.Name}");
            }
            base.OnInitialized();
        }


        protected async Task ValueChanged(T? value, EntityState state)
        {
            switch (state)
            {
                case EntityState.Deleted:
                    this.RemovedEntities.Add(value);
                    this._entities.Remove(value);
                    break;
                case EntityState.Added:
                    this.AddedEntities.Add(value);
                    if (!this._entities.Contains(value))
                    {
                        this._entities.Add(value);
                    }
                    break;
                default:
                    break;
            }

            await this._parentLayout.InvokeStateChange();
        }

        private void _parentLayout_SaveInvoked(object? sender, TParent e)
        {
            this.AddedEntities.Clear();
            this.RemovedEntities.Clear();
        }

        public void Dispose()
        {
            this._parentLayout.SaveInvoked -= this._parentLayout_SaveInvoked;
        }
    }
}
