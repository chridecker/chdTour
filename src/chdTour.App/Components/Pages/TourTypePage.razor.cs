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
    public partial class TourTypePage : BaseTourSerachForm<ITourTypeRepository, TourType>
    {
         protected override IEnumerable<GridColumDto> Columns => this._colums();
        protected override async Task OnInitializedAsync()
        {
            this.Title = PageTitleConstants.TourTypes;
            await base.OnInitializedAsync();
        }
        private IEnumerable<GridColumDto> _colums()
        => new List<GridColumDto>()
        {
             new GridColumDto
            {
                 Name = "Title",
                 SequenceNumber = 1,
                 Property = nameof(TourType.Title),
                 Width = "8rem",
                 IsFilterEnabled = true,
                 IsDefaultSorter = true,
            },
             new GridColumDto
            {
                 Name = "Skala",
                 SequenceNumber = 2,
                 Property = $"{nameof(TourType.GradeScala)}.{nameof(GradeScala.Title)}",
                 Width = "4rem",
                 IsFilterEnabled = true,
                 
            },
        };

    }
}