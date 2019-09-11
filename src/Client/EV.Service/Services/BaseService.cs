using EV.Service.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace EV.Service.Services
{
    public class BaseService
    {
        protected async Task<ResultServiceModel<T>> Post<T>(string url, object model) where T : class
        {
            ResultServiceModel<T> resultService = new ResultServiceModel<T>();
            try
            {
                HttpClient client = new HttpClient();

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
                else if(result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {

                }
                else
                {
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
            ResultServiceModel<T> resultService = new ResultServiceModel<T>();
            try
            {
                HttpClient client = new HttpClient();

                var result = await client.GetAsync(url);

                if (result.IsSuccessStatusCode)
                {
                    var json_result = await result.Content.ReadAsStringAsync();

                    T obj = GetModelFormResult<T>(json_result);

                    resultService.IsError = false;

                    resultService.Model = obj;

                    return resultService;
                }
                return null;
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
    }
}
