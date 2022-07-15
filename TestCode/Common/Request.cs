using System;
using System.Collections.Generic;
using System.IO;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Xamarin.Forms;

namespace TestCode.Common
{
    public class Request
    {
        private HttpClient ApiClient;
        public static string BaseAddress = Device.RuntimePlatform == Device.Android ? "http://10.0.2.2:5000" : "http://localhost:5001";
        public Request()
        {
            ApiClient = NetClient.Instance(BaseAddress).Client;
        }

        public bool Requests<DataResult, ParamRequest>(ref DataResult returnData, string api, ParamRequest requestParam)
        {
            if (ApiClient != null)
            {
                try
                {
                    //ApiClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", "");
                    string param = "{}";
                    if (!string.IsNullOrEmpty(requestParam.ToString()))
                    {
                        param = Newtonsoft.Json.Linq.JObject.FromObject(requestParam).ToString();
                    }

                    HttpContent content = new StringContent(param, Encoding.UTF8, "application/json");
                    HttpResponseMessage response =  ApiClient.PostAsync(api, content).Result;
                    if (response.IsSuccessStatusCode)
                    {
                        string strData = response.Content.ReadAsStringAsync().Result;
                        returnData = JsonConvert.DeserializeObject<DataResult>(strData);
                        return true;
                    }
                    else
                    {
                        return false;
                    }
                }
                catch (Exception ex)
                {
                    ex.ToString();
                    return false;
                }
            }
            return false;
        }
    }
}
