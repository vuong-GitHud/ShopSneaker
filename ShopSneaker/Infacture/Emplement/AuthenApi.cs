using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using ShopSneaker.Infacture.Helper;
using ShopSneaker.Infacture.interfaces;
using ShopSneaker.Models;

namespace ShopSneaker.Infacture.Emplement;

public class AuthenApi : IAuthenApi
{
    private readonly HttpClient httpClient;

    public AuthenApi(HttpClient httpClient)
    {
        httpClient = new HttpClient();
    }

    public async Task<AuthenViewModel> Authenticate(AuthenVm request)
    {
        string url = "/api/authen/login";
        
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://localhost:7105");
        string json = JsonConvert.SerializeObject(request);
        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(url, data);
        string responseData = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var result = JsonConvert.DeserializeObject<AuthenViewModel>(responseData);
            return result;
        }
        return new AuthenViewModel();
            
    }

    public async Task<bool> CheckEmailExist(string email)
    {
        
        string url = $"/api/accounts/CheckEmail/{email}";
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://localhost:7105");
        var response = await httpClient.GetAsync(url);
        string responseData = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var result = JsonConvert.DeserializeObject<bool>(responseData);
            return result;
        }
        return false;
    }

    public async Task<RegisterViewModel> Register(RegisterModel model)
    {
        string url = "/api/authen/register";
        
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://localhost:7105");
        string json = JsonConvert.SerializeObject(model);
        StringContent data = new StringContent(json, Encoding.UTF8, "application/json");
        var response = await httpClient.PostAsync(url, data);
        string responseData = await response.Content.ReadAsStringAsync();
        if (response.IsSuccessStatusCode)
        {
            var result = JsonConvert.DeserializeObject<RegisterViewModel>(responseData);
            return result;
        }
        return new RegisterViewModel();
    }
    
    public async Task<UserProfileViewModel> GetInfo(string userId)
    {
        string url = $"/api/accounts/Profile/{userId}";
        var httpClient = new HttpClient();
        httpClient.BaseAddress = new Uri("https://localhost:7105");
        var response = await httpClient.GetAsync(url);
        string responseData = await response.Content.ReadAsStringAsync();
        var result = new UserProfileViewModel();
        if (response.IsSuccessStatusCode)
        {
            result = JsonConvert.DeserializeObject<UserProfileViewModel>(responseData);

        }
        else
        {
            result = new UserProfileViewModel();
        }
        return result;
    }
}