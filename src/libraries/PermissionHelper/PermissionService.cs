using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using RestSharp;
using System.Text;

namespace PermissionHelper
{
    public class PermissionService : IPermissionService
    {
        private readonly string _permissionBaseUrl;

        public PermissionService(IConfiguration configuration)
        {
            _permissionBaseUrl = configuration.GetSection("PermissionServiceUrl")?.Value;
        }

        public async Task<bool> CheckAuthority(string authToken, PermissionVm permissionVm)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(authToken))
                    throw new Exception("Invalid auth token!");

                var client = new RestClient(_permissionBaseUrl);
                client.AddDefaultHeader("Authorization", authToken);
                var request = new RestRequest("/Permissions/CheckAuthority/", Method.Post);
                request.RequestFormat = DataFormat.Json;
                request.AddJsonBody(permissionVm);


                var response = await client.ExecuteAsync<PermissionResponse<object>>(request);
                var result = response == null ? default : response.Data;

                if (response.IsSuccessful)
                    return bool.Parse(result.Data.ToString());

            }
            catch (Exception e)
            {
                throw;
            }
            return false;
        }
    }

    public record PermissionVm
        (
            List<string> PermissionNames,
            int UserId,
            int BusinessProfileId
        );
}
