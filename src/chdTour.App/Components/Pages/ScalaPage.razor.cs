using chd.UI.Base.Contracts.Dtos.UltraGrid;
using chdTour.App.Components.Pages.Base;
using chdTour.Contracts.Constants;
using chdTour.DataAccess.Contracts.Domain;
using chdTour.DataAccess.Contracts.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace chdTour.App.Components.Pages
{
    public partial class ScalaPage : BaseTourSerachForm<IGradeScalaRepository, GradeScala>
    {
         protected override IEnumerable<GridColumDto> Columns => this._colums();
        protected override async Task OnInitializedAsync()
        {
            this.Title = PageTitleConstants.Skala;

            this._items = await this._repository.Where(x => true).Include(i => i.Grades).ToListAsync(this._cts.Token);

            await base.OnInitializedAsync();
        }
        protected override IQueryable<GradeScala> Include(IQueryable<GradeScala> queryable)
            => queryable.Include(i => i.Grades);
        private IEnumerable<GridColumDto> _colums()
        => new List<GridColumDto>()
        {
            new GridColumDto
            {
                 Name = "Title",
                 SequenceNumber = 1,
                 Property = nameof(GradeScala.Title),
                 Width = "10rem",
                 IsFilterEnabled = true,
                 IsDefaultSorter = false,
            },
        };
    }
}