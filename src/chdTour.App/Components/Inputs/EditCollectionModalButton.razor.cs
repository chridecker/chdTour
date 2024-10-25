using Blazored.Modal;
using chdTour.App.Components.Base;
using chdTour.DataAccess.Contracts.Domain.Base;
using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;
using Microsoft.AspNetCore.Components;
using System.Linq.Expressions;
using System.Reflection;

namespace chdTour.App.Components.Inputs
{
    public partial class EditCollectionModalButton<TRepo, TParentRepo, T, TParent, TModalForm> : BaseEditModalButton<TParentRepo, TParent>
        where TParentRepo : IBaseRepository<TParent>
        where TRepo : IBaseRepository<T>
        where T : class
        where TParent : BaseEntity<Guid>
        where TModalForm : BaseEditCollectionForm<TRepo, TParentRepo, T, TParent>
    {
        [Parameter] public ICollection<T> Items { get; set; }
        [Parameter] public TParent Value { get; set; }

        [Parameter, EditorRequired] public Expression<Func<TParent, ICollection<T>>>? Property { get; set; }

        private PropertyInfo? _propertyInfo = null;

        private List<T> _added = [];
        private List<T> _removed = [];


        protected override void OnParametersSet()
        {
            if (this.Property is not null)
            {
                var memberExpression = this.Property.Body as MemberExpression ?? throw new ArgumentException("The expression is not a member access expression.", nameof(this.Property));
                this._propertyInfo = memberExpression.Member as PropertyInfo ?? throw new ArgumentException("The member access expression does not access a property.", nameof(this.Property));
            }
            base.OnParametersSet();
        }

        protected async override Task Open()
        {
            var param = new ModalParameters
            {
                {nameof(EditCollectionModalRenderForm<TRepo,TParentRepo, T,TParent, TModalForm>.ParentLayout), this._parentLayout },
                {nameof(EditCollectionModalRenderForm<TRepo,TParentRepo, T,TParent, TModalForm>.ParentEntity), this.Value },
                {nameof(EditCollectionModalRenderForm<TRepo,TParentRepo, T,TParent, TModalForm>.Added), this._added },
                {nameof(EditCollectionModalRenderForm<TRepo,TParentRepo, T,TParent, TModalForm>.Removed), this._removed },
            };
            await this._modal.Show<EditCollectionModalRenderForm<TRepo, TParentRepo, T, TParent, TModalForm>>(this.Title, param).Result;
        }
    }
}