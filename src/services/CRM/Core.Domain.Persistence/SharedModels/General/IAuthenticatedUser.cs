using System.Collections.Generic;
using System.Threading.Tasks;

namespace Core.Domain.Persistence.SharedModels.General
{
    public interface IAuthenticatedUser
    {
        string UserId { get; }
        string UserEmail { get; }
        string UserName { get; }
        string Token { get; }
        List<string> Roles { get; }
        int? ClientBusinessProfileId { get; }
    }
}
