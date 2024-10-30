using chd.UI.Base.Components.Base;
using chd.UI.Base.Contracts.Dtos.UltraGrid;
using chdTour.App.Components.Pages.Base;
using chdTour.Contracts.Constants;
using chdTour.DataAccess.Contracts.Domain;
using chdTour.DataAccess.Contracts.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace chdTour.App.Components.Pages
{
    public partial class TourPage : BaseTourSerachForm<ITourRepository, Tour>
    {
        protected override IEnumerable<GridColumDto> Columns => this._colums();

        protected override async Task OnInitializedAsync()
        {
            this.Title = PageTitleConstants.Tours;
            await base.OnInitializedAsync();
        }
        protected override IQueryable<Tour> Include(IQueryable<Tour> queryable)
        => queryable.Include(i => i.Images).Include(i => i.Attachements);

        private IEnumerable<GridColumDto> _colums()
        => new List<GridColumDto>()
        {
             new GridColumDto
            {
                 Name = "Tour",
                 SequenceNumber = 2,
                 Property = nameof(Tour.Title),
                 IsFilterEnabled = true,
            },
             new GridColumDto
            {
                 Name = "Datum",
                 SequenceNumber = 1,
                 Property = $"{nameof(Tour.TourDate)}",
                 IsFilterEnabled = true,
                 IsDefaultSorter = true,
                 Format ="dd.MM.yy",
                 Width="7rem"
            },
        };
    }
}