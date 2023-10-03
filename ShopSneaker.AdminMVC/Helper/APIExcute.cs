using System.Net.Http.Headers;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using ShopSneaker.AdminMVC.Enum;

namespace ShopSneaker.AdminMVC.Helper
{
    public class APIExcute
    {
        private readonly HttpClient httpClient;

        public APIExcute()
        {
            httpClient = new HttpClient();
        }

        public virtual async Task<BaseResponse<TResponse>> GetData<TResponse>(string url, Dictionary<string, object> requestParams = null, string token = null)
        {
            BaseResponse<TResponse> result = new BaseResponse<TResponse>();
            try
            {
                if (requestParams != null)
                {
                    if (!url.Contains("?"))
                        url += "?";
                    foreach (KeyValuePair<string, object> k in requestParams)
                    {
                        url += $"{k.Key}={k.Value}&";
                    }
                    url = url.TrimEnd('&');

                }
                if (token != null)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                HttpResponseMessage response = await httpClient.GetAsync(url);
                string responseData = await response.Content.ReadAsStringAsync();
                result.FullResponseString = responseData;

                if (response.IsSuccessStatusCode)
                {
                    result.FullResponseString = JsonConvert.DeserializeObject<string>(responseData);

                }
                else
                {
                    result.StatusCode = response.StatusCode;
                }
                result.FullResponseString = responseData;
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.Message = ex.ToString();
            }
            return result;
        }

        public virtual async Task<BaseResponse<TResponse>> PostData<TResponse, TRequest>(string url, HttpMethodEnum method = HttpMethodEnum.POST, BaseRequest<TRequest> requestParams = null, string token = null)
        {
            BaseResponse<TResponse> result = new BaseResponse<TResponse>();
            try
            {
                if (token != null)
                {
                    httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                }

                HttpResponseMessage response = null;
                if (requestParams != null)
                {
                    string json = JsonConvert.SerializeObject(requestParams.RequestData);
                    StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
                    response = await PostDataAsync(method, url, data);
                }
                else
                {
                    response = await PostDataAsync(method, url, null);
                }
                string responseData = await response.Content.ReadAsStringAsync();
                if (response.IsSuccessStatusCode)
                {
                    result = JsonConvert.DeserializeObject<BaseResponse<TResponse>>(responseData);

                }
                else
                {
                    result.StatusCode = response.StatusCode;
                }
                result.FullResponseString = responseData;
            }
            catch (Exception ex)
            {
                result.StatusCode = HttpStatusCode.InternalServerError;
                result.Message = ex.ToString();
            }
            return result;
        }

        public virtual async Task<BaseResponse<TResponse>> PostData<TResponse, TRequest>(string url, BaseRequest<TRequest> requestParams = null, string token = null)
        {
            return await PostData<TResponse, TRequest>(url, HttpMethodEnum.POST, requestParams, token);
        }


        private Task<HttpResponseMessage> PostDataAsync(HttpMethodEnum method, string url, HttpContent content)
        {
            switch (method)
            {
                case HttpMethodEnum.POST:
                    return httpClient.PostAsync(url, content);
                case HttpMethodEnum.PUT:
                    return httpClient.PutAsync(url, content);
                case HttpMethodEnum.DELETE:
                    return httpClient.DeleteAsync(url);
            }
            return httpClient.PostAsync(url, content);
        }
    }
}
