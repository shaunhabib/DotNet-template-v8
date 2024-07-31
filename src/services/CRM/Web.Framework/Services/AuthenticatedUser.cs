
using Core.Domain.Persistence.SharedModels.General;
using Microsoft.AspNetCore.Http;
using System.Security.Claims;

namespace Web.Framework.Services
{
    public class AuthenticatedUser : IAuthenticatedUser
    {
        public AuthenticatedUser(IHttpContextAccessor httpContextAccessor)
        {
            UserEmail = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Email)?.Value;
            UserName = httpContextAccessor.HttpContext?.User?.FindFirst(ClaimTypes.Name)?.Value;
            Roles = httpContextAccessor.HttpContext?.User?.FindAll(ClaimTypes.Role).Select(x => x.Value.ToString()).ToList();
            Token = httpContextAccessor.HttpContext?.Request?.Headers["Authorization"];

            var rs = httpContextAccessor.HttpContext?.Request?.Headers.Select(s => new { s.Key, s.Value });

            string clientBpId = rs?.FirstOrDefault(f => f.Key?.ToLower() == "clientbusinessprofileid")?.Value;
            ClientBusinessProfileId = string.IsNullOrWhiteSpace(clientBpId) ? null : int.Parse(clientBpId);

            string userId = rs?.FirstOrDefault(f => f.Key?.ToLower() == "userid")?.Value;
            UserId = string.IsNullOrWhiteSpace(userId) ? null : userId;
        }

        public string UserId { get; }
        public string UserEmail { get; }
        public string UserName { get; }
        public List<string> Roles { get; }
        public string Token { get; }
        public int? ClientBusinessProfileId { get; }
    }
}
