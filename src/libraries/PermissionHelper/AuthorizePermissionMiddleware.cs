using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.Features;

namespace PermissionHelper
{
    public class AuthorizePermissionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IPermissionService _permissionService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AuthorizePermissionMiddleware(RequestDelegate next, IPermissionService permissionService, IHttpContextAccessor httpContextAccessor)
        {
            _next = next;
            _permissionService = permissionService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                #region Http headers
                var token = context?.Request?.Headers["Authorization"];

                var rs = _httpContextAccessor.HttpContext?.Request?.Headers.Select(s => new { s.Key, s.Value });

                string clientBpId = rs?.FirstOrDefault(f => f.Key?.ToLower() == "clientbusinessprofileid")?.Value;
                int ClientBusinessProfileId = string.IsNullOrWhiteSpace(clientBpId) ? 0 : int.Parse(clientBpId);

                string userId = rs?.FirstOrDefault(f => f.Key?.ToLower() == "userid")?.Value;
                int UserId = string.IsNullOrWhiteSpace(userId) ? 0 : int.Parse(userId);
                #endregion


                if (ClientBusinessProfileId <= 0 || rs == null)
                    throw new UnauthorizedAccessException();

                #region Permission name
                var allAttributes = context.Features.Get<IEndpointFeature>()?.Endpoint?.Metadata?.ToList();
                var permissionAttribute = allAttributes?.FirstOrDefault(m => m is PermissionAttribute);

                string[] permissionName = permissionAttribute != null
                    ? ((PermissionAttribute)permissionAttribute)?.Name
                    : null;
                #endregion

                bool processcall = rs == null ? false : bool.Parse((rs?.FirstOrDefault(f => f.Key?.ToLower() == "processcall")?.Value));

                #region Authority check
                var param = new PermissionVm
                (
                    permissionName?.ToList(),
                    UserId,
                    ClientBusinessProfileId
                );

                bool IsAuthorize = processcall == false ? false : await _permissionService.CheckAuthority(token, param);
                #endregion

                if (!processcall || !IsAuthorize)
                    throw new UnauthorizedAccessException();

            }
            catch (Exception e)
            {
                throw new UnauthorizedAccessException("Unauthorized login attempt");
            }
            await _next(context);
        }
    }
}
