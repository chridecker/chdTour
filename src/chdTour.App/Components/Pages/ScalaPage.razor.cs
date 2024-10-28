using chd.UI.Base.Components.Base;
using chdTour.App.Components.Base;
using chdTour.Contracts.Constants;
using chdTour.DataAccess.Contracts.Domain;
using chdTour.DataAccess.Contracts.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace chdTour.App.Components.Pages
{
    public partial class ScalaPage : BaseSearchForm<IGradeScalaRepository, GradeScala>
    {
        protected override async Task OnInitializedAsync()
        {
            this.Title = PageTitleConstants.Skala;

            this._items = await this._repository.Where(x => true).Include(i => i.Grades).ToListAsync(this._cts.Token);

            await base.OnInitializedAsync();
        }
        protected override IQueryable<GradeScala> Include(IQueryable<GradeScala> queryable)
            => queryable.Include(i => i.Grades);
    }
}