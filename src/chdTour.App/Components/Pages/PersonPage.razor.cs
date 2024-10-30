using chd.UI.Base.Components.Base;
using chd.UI.Base.Components.Base.Edit;
using chd.UI.Base.Contracts.Dtos.UltraGrid;
using chdTour.App.Components.Pages.Base;
using chdTour.Contracts.Constants;
using chdTour.DataAccess.Contracts.Domain;
using chdTour.DataAccess.Contracts.Interfaces.Repositories;
using Microsoft.AspNetCore.Components;
using Microsoft.EntityFrameworkCore;

namespace chdTour.App.Components.Pages
{
    public partial class PersonPage : BaseTourSerachForm<IPersonRepository, DataAccess.Contracts.Domain.Person>
    {
        protected override IEnumerable<GridColumDto> Columns => this._colums();
        protected override async Task OnInitializedAsync()
        {
            this.Title = PageTitleConstants.Persons;
            await base.OnInitializedAsync();
        }
        private IEnumerable<GridColumDto> _colums()
        => new List<GridColumDto>()
        {
            new GridColumDto
            {
                 Name = "Name",
                 SequenceNumber = 1,
                 Property = nameof(DataAccess.Contracts.Domain.Person.FullName),
                 Width = "10rem",
                 IsFilterEnabled = true,
                  IsDefaultSorter = false,
            },
           
        };
    }
}