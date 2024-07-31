using Core.Domain.Persistence.SharedModels.Entities;
using Core.Domain.Persistence.SharedModels.Enum;
using Core.Domain.Persistence.SharedModels.General;
using Core.Domain.Persistence.SharedModels.Wrappers;
using Newtonsoft.Json;
using RestSharp;

namespace Infrastructure.Persistence.Helper
{
    public class HttpApiHelper
    {
        #region ctor
        private readonly IAuthenticatedUser _authenticatedUser;
        private string Token;
        private string ClientBusinessProfileId;
        private string UserId;

        public HttpApiHelper(IAuthenticatedUser authenticatedUser)
        {
            _authenticatedUser = authenticatedUser;
            Token = _authenticatedUser.Token;
            ClientBusinessProfileId = _authenticatedUser.ClientBusinessProfileId.ToString();
            UserId = _authenticatedUser.UserId;
        }
        #endregion


        public async Task<(T data, string Message)> GetApi<T>(string apiBasicUri, string url)
        {
            try
            {
                var client = new RestClient(apiBasicUri);
                var request = new RestRequest(url, Method.Get);
                request.RequestFormat = DataFormat.Json;
                request.AddHeader("Authorization", Token);
                request.AddHeader("processcall", "true");
                request.AddHeader("clientbusinessprofileid", ClientBusinessProfileId);
                request.AddHeader("userId", UserId);
                var response = await client.ExecuteAsync<Response<T>>(request);

                if (response.IsSuccessful)
                {
                    var result = response.Data;
                    if (result != null)
                    {
                        return (result.Data, result.Message);
                    }
                }
                return (default(T), response.ErrorMessage);
            }
            catch (Exception ex)
            {
                return (default(T), ex.Message);
            }
        }


        public async Task<(object data, string Message)> PostApi<T>(ApiPostObj apiPostObj, T contentValue)
        {
            try
            {
                var client = new RestClient(apiPostObj.ApiBasicUrl);
                var request = new RestRequest(apiPostObj.Url, Method.Post);
                request.AddHeader("Authorization", Token);
                request.AddHeader("processcall", "true");
                request.AddHeader("clientbusinessprofileid", ClientBusinessProfileId);
                request.AddHeader("userId", UserId);
                if (apiPostObj.ApiPostCategoryType == ApiPostCategoryType.MultiPostFormData)
                {
                    request.AddParameter("Data", JsonConvert.SerializeObject(contentValue));

                    request.AlwaysMultipartFormData = true;
                }
                else if (apiPostObj.ApiPostCategoryType == ApiPostCategoryType.ApplicationJson)
                {
                    request.RequestFormat = DataFormat.Json;
                    request.AddBody(contentValue);
                }

                var response = await client.ExecuteAsync<Response<object>>(request);
                if (response.IsSuccessful)
                {
                    var result = response.Data;
                    if (result != null)
                    {
                        return (result.Data, response.Data.Message);
                    }
                }
                return (null, response.ErrorMessage);

            }
            catch (Exception ex)
            {
                return (null, ex.Message);
            }
        }

        
    }
}
