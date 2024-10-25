using chd.UI.Base.Client.Implementations.Authorization;
using chd.UI.Base.Contracts.Dtos.Authentication;
using chd.UI.Base.Contracts.Interfaces.Authentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace chdTour.App.Implementations
{
    public class chdTourProfileService : ProfileService<int, int>, IchdTourProfileService
    {
        protected override Task<UserPermissionDto<int>> GetPermissions(UserDto<int, int> dto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        protected override Task<UserDto<int, int>> GetUser(LoginDto<int> dto, CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }
    }
    public interface IchdTourProfileService : IProfileService<int,int>
    {

    }
}
