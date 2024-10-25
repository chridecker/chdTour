using System.Reflection;
using Blazored.Modal;
using Blazored.Modal.Services;
using chdTour.App.Components.Base;
using chd.UI.Base.Components.Extensions;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;
using chdTour.DataAccess.Contracts.Interfaces.Repositories.Base;
using chdTour.DataAccess.Contracts.Domain.Base;
using chd.UI.Base.Contracts.Enum;

namespace chdTour.App.Components.Inputs
{
    public partial class EditMultiAssignment<TRepo, TParentRepo, T, TParent, TForm> : BaseEditCollectionInput<TParentRepo, T, TParent>
        where TRepo : IBaseRepository<T>
        where TParentRepo : IBaseRepository<TParent>
        where T : class
        where TParent : class
        where TForm : BaseEditForm<TRepo, T>
    {

        [Inject] private IModalService _modal { get; set; }
        [Inject] private TRepo _repo { get; set; }

        [Parameter] public string EntityName { get; set; }

        private IEnumerable<T> Items => this._entities;

        private async Task FieldValueChanged(T item, object value, PropertyInfo propInfo)
        {
            propInfo.SetValue(item, value);
            await this.ValueChanged(item, EntityState.Modified);
        }

        public async Task New()
        {
            var newEntity = Activator.CreateInstance<T>();
            if (newEntity is BaseEntity<Guid> entity)
            {
                entity.Id = Guid.NewGuid();
            }
            this._propertyInfoOneAssign.SetValue(newEntity, this.ParentEntity);
            if (!await this.OpenModal(newEntity))
            {
                await this._repo.DeleteAsync(newEntity, this.Token);
                return;
            }
            await this.ValueChanged(newEntity, EntityState.Added);
        }

        public async Task Edit(T entity)
        {
            if (await this.OpenModal(entity))
            {
                await this.ValueChanged(entity, EntityState.Modified);
            }
            else
            {
            }
        }
        public async Task Delete(T entity)
        {
            var dialog = await this._modal.ShowDialog("Löschen?", EDialogButtons.YesNo);
            if (dialog == EDialogResult.Yes)
            {
                await this._repo.DeleteAsync(entity, this.Token);
                await this.ValueChanged(entity, EntityState.Deleted);
            }
        }


        private async Task<bool> OpenModal(T entity)
        {
            var param = new ModalParameters()
            {
                {nameof(BaseEditForm<TRepo, T>.Value),entity }
            };
            var result = await this._modal.Show<TForm>(param).Result;
            return result.Confirmed;
        }
    }
}