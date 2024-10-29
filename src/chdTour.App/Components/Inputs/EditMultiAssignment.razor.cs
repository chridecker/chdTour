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
        where TParent : BaseEntity<Guid>
        where TForm : BaseEditForm<TRepo, T>
    {

        [Inject] private IModalService _modal { get; set; }
        [Inject] private TRepo _repo { get; set; }

        [Parameter] public string EntityName { get; set; }
        [Parameter] public RenderFragment? HeaderContent { get; set; }
        [Parameter] public RenderFragment<T>? RowTemplate { get; set; }

        private IEnumerable<T> Items => this._entities;

        public async Task New()
        {
            var newEntity = Activator.CreateInstance<T>();
            if (newEntity is BaseEntity<Guid> entity)
            {
                entity.Id = Guid.NewGuid();
            }
            this._propertyInfoOneAssign.SetValue(newEntity, this.ParentEntity);
            if (await this.OpenModal(newEntity))
            {
                this._entities.Add(newEntity);
                await this.ValueChanged(newEntity, EntityState.Added);
            }
        }

        public async Task Edit(T entity)
        {
            if (await this.OpenModal(entity))
            {
                await this.ValueChanged(entity, EntityState.Added);
            }
        }

        public async Task Delete(T entity)
        {
            if (await this._modal.ShowDialog($"Eintrag Löschen?", EDialogButtons.YesNo) == EDialogResult.Yes)
            {
                await this._repo.DeleteAsync(entity, this.Token);
                this._entities.Remove(entity);
            }
        }

        protected override async Task ValueChanged(T? value, EntityState state)
        {
            await this.OnChange?.Invoke(value, state);
            if (state == EntityState.Added)
            {
                await this._repo.SaveAsync(value, this.Token);
            }
            await base.ValueChanged(value, state);
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