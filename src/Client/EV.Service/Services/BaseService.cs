using EV.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace EV.Service.Services
{
    public class BaseService
    {
        //private readonly AuthService _authService = new AuthService();
        protected async Task<ResultServiceModel<T>> Post<T>(string url, object model) where T : class
        {
            //StartMethod:
            ResultServiceModel<T> resultService = new ResultServiceModel<T>();
            try
            {
                HttpClient client = new HttpClient();

                string token = "";

                //try
                //{
                //    token = await SecureStorage.GetAsync("Token");
                //}
                //catch(Exception e)
                //{
                //    throw e;
                //}

               // client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                HttpContent content = GetHttpContent(model);

                var result = await client.PostAsync(url, content);

                if (result.IsSuccessStatusCode)
                {
                    var json_result = await result.Content.ReadAsStringAsync();

                    T obj = GetModelFormResult<T>(json_result);

                    resultService.IsError = false;

                    resultService.Model = obj;

                    return resultService;
                }
                //else if(result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                //{
                //    try
                //    {
                //        string refreshToken = await SecureStorage.GetAsync("RefreshToken");
                //        string email = await SecureStorage.GetAsync("Email");
                //        var tokenData = await _authService.GetTokenByRefreshToken(email, refreshToken);
                //        if(tokenData != null || !tokenData.IsError)
                //        {
                //            if(tokenData.Model == null || tokenData.Model.Token == null)
                //            {
                //                CloseApp();
                //            }
                //            await SecureStorage.SetAsync("Token", tokenData.Model.Token);
                //            goto StartMethod;
                //        }
                //        else
                //        {
                //            resultService.IsError = true;
                //        }
                //    }
                //    catch(Exception e)
                //    {
                //        CloseApp();
                //    }
                //}
                else
                {
                    client.Dispose();
                    return null;
                }
            }
            catch (Exception e)
            {
                resultService.IsError = true;
            }
            return resultService;
        }

        protected async Task<ResultServiceModel<T>> Get<T>(string url) where T : class
        {
            //StartMethod:
            ResultServiceModel<T> resultService = new ResultServiceModel<T>();
            try
            {
                HttpClient client = new HttpClient();

                string token = "";

                //try
                //{
                //    token = await SecureStorage.GetAsync("Token");
                //}
                //catch (Exception e)
                //{
                //    throw e;
                //}

                //client.DefaultRequestHeaders.Authorization = new System.Net.Http.Headers.AuthenticationHeaderValue("Bearer", token);

                var result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    var json_result = await result.Content.ReadAsStringAsync();

                    T obj = GetModelFormResult<T>(json_result);

                    resultService.IsError = false;

                    resultService.Model = obj;

                    return resultService;
                }
                //else if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                //{
                //    try
                //    {
                //        string refreshToken = await SecureStorage.GetAsync("RefreshToken");
                //        string email = await SecureStorage.GetAsync("Email");
                //        var tokenData = await _authService.GetTokenByRefreshToken(email, refreshToken);
                //        if (tokenData != null || !tokenData.IsError)
                //        {
                //            if (tokenData.Model == null || tokenData.Model.Token == null)
                //            {
                //                CloseApp();
                //            }
                //            await SecureStorage.SetAsync("Token", tokenData.Model.Token);
                //            goto StartMethod;
                //        }
                //        else
                //        {
                //            resultService.IsError = true;
                //        }
                //    }
                //    catch (Exception e)
                //    {
                //        CloseApp();
                //    }
                //}
                else
                {
                    client.Dispose();
                    return null;
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
