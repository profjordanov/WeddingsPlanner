using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.JSInterop;
using WeddingsPlanner.Blazor.Client.Infrastructure.Core;
using WeddingsPlanner.Core.Models;

namespace WeddingsPlanner.Blazor.Client.Infrastructure.Business
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly IApplicationState _applicationState;

        public ApiClient(HttpClient httpClient, IApplicationState applicationState)
        {
            _httpClient = httpClient;
            _applicationState = applicationState;
        }

        public async Task<JwtModel> UserLogin(LoginUserModel request)
        {
            try
            {
                var response = await _httpClient.PostAsync(
                    "api/account/login",
                    new FormUrlEncodedContent(
                        new List<KeyValuePair<string, string>>
                        {
                            new KeyValuePair<string, string>("email", request.Email),
                            new KeyValuePair<string, string>("password", request.Password),
                        }));

                var responseString = await response.Content.ReadAsStringAsync();
                if (!response.IsSuccessStatusCode)
                {
                    //return new ApiResponse<UserLoginResponseModel>(new ApiError("Server error " + (int)response.StatusCode, responseString));
                }

                return Json.Deserialize<JwtModel>(responseString);
            }
            catch (Exception ex)
            {
                //return new ApiResponse<UserLoginResponseModel>(new ApiError("HTTP Client", ex.Message));
                return new JwtModel();
            }
        }
    }
}