using EV.Service.Models;
using EWalletV2.Api.ViewModels.Auth;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EV.Service.Services
{
    public class TokenService
    {
        public async Task<ResultServiceModel<GetTokenByRefreshTokenViewModel>> GetAccessToken()
        {
            ResultServiceModel<GetTokenByRefreshTokenViewModel> resultService = new ResultServiceModel<GetTokenByRefreshTokenViewModel>();
            try
            {
                string url = Helper.BaseUrl + "auth/GetTokenByRefreshToken";

                HttpClient client = new HttpClient();

                string refreshToken = "";

                string email = "";

                try
                {
                    refreshToken = SecureStorage.GetAsync("RefreshToken").Result;
                    email = SecureStorage.GetAsync("Email").Result;
                }
                catch (Exception e)
                {
                    CloseApp();
                }

                GetTokenByRefreshTokenCommand model = new GetTokenByRefreshTokenCommand
                {
                    Email = email,

                    RefreshToken = refreshToken

                };

                HttpContent content = GetHttpContent(model);

                var result = await client.PostAsync(url, content);

                if (result.IsSuccessStatusCode)
                {
                    var json_result = await result.Content.ReadAsStringAsync();

                    GetTokenByRefreshTokenViewModel obj = GetModelFormResult<GetTokenByRefreshTokenViewModel>(json_result);

                    resultService.IsError = false;

                    resultService.Model = obj;

                    return resultService;
                }
                else
                {
                    client.Dispose();
                    CloseApp();
                }
            }
            catch (Exception e)
            {
                resultService.IsError = true;
            }
            return resultService;
        }

        protected HttpContent GetHttpContent(object model)
        {
            string json = JsonConvert.SerializeObject(model);
            return new StringContent(json, Encoding.UTF8, "application/json");
        }

        protected T GetModelFormResult<T>(string json_result) where T : class
        {

            return JsonConvert.DeserializeObject<T>(json_result);
        }

        private void CloseApp()
        {
            SecureStorage.RemoveAll();
            Environment.Exit(0);
        }
    }
}
